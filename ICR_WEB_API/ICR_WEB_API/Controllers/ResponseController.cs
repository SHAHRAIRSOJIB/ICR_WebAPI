using ClosedXML.Excel;
using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseRepo _responseRepo;
        public ResponseController(IResponseRepo responseRepo)
        {
            _responseRepo = responseRepo;
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> DownloadExcel()
        {
            var responses = await _responseRepo.GetAllFormatedResponse();

            var questionGroups = responses
                .SelectMany(r => r.QuestionWithAnswers)
                .GroupBy(q => new { q.QuestionId, q.QuestionText, q.Type, q.SortOrder })
                .Select(g => new
                {
                    QuestionId = g.Key.QuestionId,
                    QuestionText = g.Key.QuestionText,
                    Type = g.Key.Type,
                    SortOrder = g.Key.SortOrder,
                    // For rating type, get distinct RatingItemText; for option type, get distinct SelectedOptionText.
                    Headers = g.Key.Type == Service.Enum.QuestionType.Rating
                        ? g.Select(a => a.RatingItemText).Where(text => !string.IsNullOrEmpty(text)).Distinct().ToList()
                        : g.Select(a => a.SelectedOptionText).Where(text => !string.IsNullOrEmpty(text)).Distinct().ToList()
                })
                .OrderBy(x => x.SortOrder)
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Responses");
                int col = 1;
                int row = 1;

                worksheet.Cell(1, col++).Value = "ResponseId";
                worksheet.Cell(1, col++).Value = "SubmissionDate";
                worksheet.Cell(1, col++).Value = "ShopName";
                worksheet.Cell(1, col++).Value = "OwnerName";
                worksheet.Cell(1, col++).Value = "DistrictName";
                worksheet.Cell(1, col++).Value = "StreetName";
                worksheet.Cell(1, col++).Value = "UnifiedLicenseNumber";
                worksheet.Cell(1, col++).Value = "LicenseIssueDateLabel";
                worksheet.Cell(1, col++).Value = "OwnerIDNumber";
                worksheet.Cell(1, col++).Value = "AIESECActivity";
                worksheet.Cell(1, col++).Value = "Municipality";
                worksheet.Cell(1, col++).Value = "FullAddress";
                worksheet.Cell(1, col++).Value = "ImageLicensePlate";
                worksheet.Cell(1, col++).Value = "IsAnswerSubmitted";

                // For each question, create dynamic headers based on the maximum count.
                foreach (var q in questionGroups)
                {
                    foreach (var header in q.Headers)
                    {
                        // Here we set the header using the actual rating or option text.
                        worksheet.Cell(row, col++).Value = $"{q.QuestionText} - {header}";
                    }
                }

                row = 2;
                foreach (var response in responses)
                {
                    col = 1;
                    worksheet.Cell(row, col++).Value = response.ResponseId;
                    worksheet.Cell(row, col++).Value = response.SubmissionDate;
                    worksheet.Cell(row, col++).Value = response.ShopName;
                    worksheet.Cell(row, col++).Value = response.OwnerName;
                    worksheet.Cell(row, col++).Value = response.DistrictName;
                    worksheet.Cell(row, col++).Value = response.StreetName;
                    worksheet.Cell(row, col++).Value = response.UnifiedLicenseNumber;
                    worksheet.Cell(row, col++).Value = response.LicenseIssueDateLabel;
                    worksheet.Cell(row, col++).Value = response.OwnerIDNumber;
                    worksheet.Cell(row, col++).Value = response.AIESECActivity;
                    worksheet.Cell(row, col++).Value = response.Municipality;
                    worksheet.Cell(row, col++).Value = response.FullAddress;
                    worksheet.Cell(row, col++).Value = response.ImageLicensePlate;
                    worksheet.Cell(row, col++).Value = response.IsAnswerSubmitted;

                    foreach (var q in questionGroups)
                    {
                        foreach (var header in q.Headers)
                        {
                            // Find an answer that matches the header for the current question.
                            var answer = response.QuestionWithAnswers
                                .Where(a => a.QuestionId == q.QuestionId)
                                .FirstOrDefault(a =>
                                    (q.Type == Service.Enum.QuestionType.Rating && a.RatingItemText == header) ||
                                    (q.Type != Service.Enum.QuestionType.Rating && a.SelectedOptionText == header));

                            // For rating questions, output the rating value; for options, output the option text.
                            string cellValue = string.Empty;
                            if (answer != null)
                            {
                                cellValue = q.Type == Service.Enum.QuestionType.Rating ? answer.RatingItemValue : answer.SelectedOptionText;
                            }
                            worksheet.Cell(row, col++).Value = cellValue;
                        }
                    }
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Responses.xlsx");
                }
            }
        }

        [HttpGet("GetAllFormated")]
        public async Task<IActionResult> GetAllFormated()
        {
            var data = await _responseRepo.GetAllFormatedResponse();
            if (data.Count > 0)
                return Ok(data);
            else
                return NotFound(new { message = "No Response Data found" });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _responseRepo.GetAll();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResponseById(int id)
        {
            var data = await _responseRepo.GetById(id);
            if (data != null && data.Id != 0)
            {
                return Ok(data);
            }

            return NotFound(new { message = "Data not found" });
        }

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(ResponseUpdateStatusDTO entity)
        {
            if (entity == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            var response = await _responseRepo.GetByUnifiedLicenseNumber(entity.UnifiedLicenseNumber);

            if (response == null)
            {
                return NotFound(new
                {
                    Message = "User response not exist"
                });
            }

            var updatedResponse = await _responseRepo.UpdateStatus(entity.UnifiedLicenseNumber, entity.IsAnswerSubmitted);

            if (updatedResponse == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful response status update"
                });
            }

            return Ok(new
            {
                Message = "Successful response status update",
                Response = updatedResponse
            }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Save(Response entity)
        {
            if (entity == null || entity.UserId <= 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            var response = await _responseRepo.GetByUnifiedLicenseNumber(entity.UnifiedLicenseNumber);

            if (response != null && response.IsAnswerSubmitted == false)
            {
                return Ok(new
                {
                    Message = "Already exist but answers not submitted",
                    Response = response
                });
            }
            else if (response != null && response.IsAnswerSubmitted == true)
            {
                return BadRequest(new
                {
                    Message = "Already submitted"
                });
            }

            var result = await _responseRepo.Save(entity);

            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful Save"
                });
            }

            return Ok(result);
        }
    }
}
