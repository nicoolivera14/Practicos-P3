using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Practica8EP_API.Datos;
using Practica8EP_API.Modelos;
using Practica8EP_API.Modelos.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Practica8EP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EPController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EPDto>> GetEPDto()
        {
            return Ok(EPStore.EPList);
        }

        [HttpGet("Id", Name = "GetEp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EPDto> GetEPDto(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var ep = EPStore.EPList.FirstOrDefault(a => a.Id == id);

            if (ep == null)
            {
                return NotFound();
            }

            return Ok(ep);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EPDto> CrearEPDto([FromBody] EPDto epdto)
        {
            if (epdto == null)
            {
                return BadRequest(epdto);
            }
            if (epdto.ID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            epdto.ID = EPStore.EPList.OrderByDescending(e => e.ID).FirstOrDefault().ID + 1;
            EPStore.EPList.Add(epdto);
            return CreatedAtRoute("GetEP", new { id = epdto.ID }, epdto);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEP(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var ep = EPStore.EPList.FirstOrDefault(v => v.ID == id);
            if (ep == null)
            {
                return NotFound();
            }
            EPStore.EPList.Remove(ep);
            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEP(int id, [FromBody] EPDto epdto)
        {
            if (epdto == null)
            {
                return BadRequest();
            }
            var ep = EPStore.EPList.FirstOrDefault(v => v.ID == id);
            ep.Nombre = epdto.Nombre;
            ep.Ocupantes = epdto.Ocupantes;
            ep.MetrosCuadrados = epdto.MetrosCuadrados;
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEP(int id, JsonPatchDocument<EPDto> patchdto)
        { if (patchdto == null || id == 0) 
            {
                return BadRequest();
            }
        var ep = EPStore.EPList.FirstOrDefault(v => v.ID == id);
            patchdto.ApplyTo(ep, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}


