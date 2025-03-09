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
            var formattedResponse = await _responseRepo.GetAllFormatedResponse();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Responses");
                var _row = 1;
                var _col = 1;

                worksheet.Cell(_row, _col++).Value = "Response Id";
                worksheet.Cell(_row, _col++).Value = "Submission Date";
                worksheet.Cell(_row, _col++).Value = "Shop Name";
                worksheet.Cell(_row, _col++).Value = "Owner Name";
                worksheet.Cell(_row, _col++).Value = "District Name";
                worksheet.Cell(_row, _col++).Value = "Street Name";
                worksheet.Cell(_row, _col++).Value = "Unified License Number";
                worksheet.Cell(_row, _col++).Value = "License Issue Date Label";
                worksheet.Cell(_row, _col++).Value = "Owner ID Number";
                worksheet.Cell(_row, _col++).Value = "AIESEC Activity";
                worksheet.Cell(_row, _col++).Value = "Municipality";
                worksheet.Cell(_row, _col++).Value = "Full Address";
                worksheet.Cell(_row, _col++).Value = "Image License Plate";
                worksheet.Cell(_row, _col++).Value = "Is Answer Submitted";
                worksheet.Cell(_row, _col++).Value = "User Email";

                // Write headers
                for (int i = 0; i < formattedResponse.Columns.Count; i++)
                {
                    worksheet.Cell(_row, _col++).Value = formattedResponse.Columns[i].DisplayLabel;
                }

                // Write rows
                for (int rowIndex = 0; rowIndex < formattedResponse.Rows.Count; rowIndex++)
                {
                    var row = formattedResponse.Rows[rowIndex];
                    var col = 1;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.ResponseId;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.SubmissionDate;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.ShopName;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.OwnerName;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.DistrictName;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.StreetName;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.UnifiedLicenseNumber;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.LicenseIssueDateLabel;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.OwnerIDNumber;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.AIESECActivity;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.Municipality;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.FullAddress;
                    worksheet.Cell(rowIndex + 2, col++).Value = $"{Request.Scheme}://{Request.Host}" + row.ImageLicensePlate;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.IsAnswerSubmitted;
                    worksheet.Cell(rowIndex + 2, col++).Value = row.User != null && !String.IsNullOrEmpty(row.User.Email) ? row.User.Email : "";

                    for (int colIndex = 0; colIndex < formattedResponse.Columns.Count; colIndex++)
                    {
                        var columnKey = formattedResponse.Columns[colIndex].UniqueKey;
                        worksheet.Cell(rowIndex + 2, colIndex + col).Value = row.Answers[columnKey];
                    }
                }

                // Save or return workbook as needed.
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
            if (data != null)
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
