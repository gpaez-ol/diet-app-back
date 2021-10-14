using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Utils.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;
using Outland.Utils.Pagination;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("biometric")]
    [ApiController]
    public class BiometricController : ControllerBase
    {
        private readonly BiometricLogic _biometricLogic;

        public BiometricController(BiometricLogic biometricLogic)
        {
            _biometricLogic = biometricLogic;
        }

        /// <summary>
        /// Get Biometrics implements pagination
        /// </summary>
        /// <returns> Ok({ totalPages: Total number of pages, pages: [] array of Biometrics }) </returns>
        [ProducesResponseType(typeof(IPaginationResult<DietItemDTO>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpGet("list/{userId}")]
        public ActionResult GetBiometrics([FromQuery] PaginationDataParams pagination,Guid userId)
        {
            var biometrics = _biometricLogic.GetBiometrics(pagination,userId);
            return Ok(biometrics);
        }

        /// <summary>
        /// GetBiometric by Id
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BiometricDTO), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBiometricById(Guid id)
        {
            var biometricDTO = await _biometricLogic.GetBiometricById(id);
            return Ok(biometricDTO);
        }


        /// <summary>
        /// Creates a new Diet
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateBiometric(BiometricCreateDTO newBiometric,Guid userId)
        {
            await _biometricLogic.CreateBiometric(newBiometric,userId);
            return Ok();
        }

        /// <summary>
        /// Update Biometric
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPut("{biometricId}")]
        public async Task<ActionResult> UpdateBiometric(BiometricCreateDTO biometric, Guid biometricId)
        {
            await _biometricLogic.UpdateBiometric(biometric, biometricId);
            return Ok();
        }

        /// <summary>
        /// Delete Biometric
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpDelete("{biometricId}")]
        public async Task<ActionResult> DeleteBiometric(Guid biometricId)
        {
            await _biometricLogic.DeleteBiometric(biometricId);
            return Ok();
        }
    }
}