using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.DTOs;
using TechnicalTest.Core;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;
using System.Text.Json;

namespace TechnicalTest.API.Controllers
{
    /// <summary>
    /// Shape Controller which is responsible for calculating coordinates and grid value.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Constructor of the Shape Controller.
        /// </summary>
        /// <param name="shapeFactory"></param>
        public ShapeController(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        /// <summary>
        /// Calculates the Coordinates of a shape given the Grid Value.
        /// </summary>
        /// <param name="calculateCoordinatesRequest"></param>   
        /// <returns>A Coordinates response with a list of coordinates.</returns>
        /// <response code="200">Returns the Coordinates response model.</response>
        /// <response code="400">If an error occurred while calculating the Coordinates.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody]CalculateCoordinatesDTO calculateCoordinatesRequest)
        {
            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            ShapeEnum shape = (ShapeEnum)calculateCoordinatesRequest.ShapeType;
            if (shape == ShapeEnum.None || shape != ShapeEnum.Triangle) return BadRequest();
            // TODO: Call the Calculate function in the shape factory.
            var grid = new Grid(calculateCoordinatesRequest.Grid.Size);
            var gridValue = new GridValue(calculateCoordinatesRequest.GridValue);
            var result = _shapeFactory.CalculateCoordinates(shape,  grid, gridValue);
            // TODO: Return BadRequest with error message if the calculate result is null
            if (result == null) return BadRequest("ERROR:: No Response Created.");
            // TODO: Create ResponseModel with Coordinates and return as OK with responseModel.
            var JsonResponse = JsonSerializer.Serialize(result);
            return Ok(JsonResponse);
        }

        /// <summary>
        /// Calculates the Grid Value of a shape given the Coordinates.
        /// </summary>
        /// <remarks>
        /// A Triangle Shape must have 3 vertices, in this order: Top Left Vertex, Outer Vertex, Bottom Right Vertex.
        /// </remarks>
        /// <param name="gridValueRequest"></param>   
        /// <returns>A Grid Value response with a Row and a Column.</returns>
        /// <response code="200">Returns the Grid Value response model.</response>
        /// <response code="400">If an error occurred while calculating the Grid Value.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody]CalculateGridValueDTO gridValueRequest)
        {
            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            ShapeEnum shapeEnum = (ShapeEnum)gridValueRequest.ShapeType;
            if (shapeEnum == ShapeEnum.None || shapeEnum != ShapeEnum.Triangle) return BadRequest();
            // TODO: Create new Shape with coordinates based on the parameters
            List<Coordinate> newList = gridValueRequest.Vertices.Select(el => new Coordinate(el.x, el.y)).ToList();
            var shape = new Shape(newList);
            // TODO: Call the function in the shape factory to calculate grid value.
            var grid = new Grid(gridValueRequest.Grid.Size);
            var result = _shapeFactory.CalculateGridValue(shapeEnum, grid, shape);
            // TODO: If the GridValue result is null then return BadRequest with an error message.
            if (result == null) return BadRequest("ERROR:: No Response Created.");
            // TODO: Generate a ResponseModel based on the result and return it in Ok();
            var JsonResponse = JsonSerializer.Serialize(result);
            return Ok(JsonResponse);
        }
    }
    
}
