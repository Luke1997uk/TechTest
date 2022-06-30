using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;
using System.Diagnostics;

namespace TechnicalTest.Core.Services

{
    public class ShapeService : IShapeService
    {
        public Shape ProcessTriangle(Grid grid, GridValue gridValue)
        {
            // TODO: Calculate the coordinates.

            //Coverted row string to Int using Chars ASCII numerical Values ( Each Char is Store as a number )
            // e.g Uppercase A = 65
            int rowY = (char.ToUpper(char.Parse(gridValue.Row)) - 64) * grid.Size;
            int colX = gridValue.Column;

            // Check If colum number is even. 
            if (colX % 2 == 0)
            {
                //Find the colum / X Bottom Right Vertex 
                colX = (colX / 2) * grid.Size;


                //Now with all the information for the Bottom Right Vertex the other two vertex points can be worked out 

                return new Shape(new List<Coordinate> {
                    //One grid place back for both X and Y Top Left Vertex.
                    //Directly across from Bottom Right Vertex. so minus both axis by grid size 
                    new(colX - grid.Size, rowY - grid.Size),
                    //One Grid place back for just Y, Second Point Outer Vertex 
                    //I Know it has to be the Even number location, thus the outer vertex Y axis is lower than the bottom right Y axis 
                    new(colX, rowY - grid.Size),
                    //Bottom Right Vertex
                    new(colX, rowY) });
            }
            else
            {
                //Find the colum / X Bottom Right Vertex. However add one to make the number even 
                colX = ((colX + 1) / 2) * grid.Size;

                return new Shape(new List<Coordinate>
                {
                    //One grid place back for both X and Y, Top Left Vertex 
                    //Directly across from Bottom Right Vertex. so minus both axis by grid size 
                    new(colX - grid.Size, rowY - grid.Size),
                    //One Grid place back for just X, Second Point Outer Vertex 
                    //I Know it has to be the Odd number location, thus the outer vertex X axis is lower than the bottom right X axis
                    new(colX - grid.Size, rowY),
                    //Thrid Point Bottom Right
                    new(colX, rowY) });
            }
        }

    

        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {
            // TODO: Calculate the grid value.

            //Tekes Top left Y vertex and transforms it into the Row Value.
            //E.g.Y = 20 + 10 (grid.size) = 30  30 / 10 = 3 = C
            int gridValueRow = (triangle.TopLeftVertex.Y + grid.Size) / grid.Size;

            //Find col by adding Top left X and Bottom right X and divide by grid size to get number.
            //E.g X = 10 + 20 = 30  30 / 10 = 3
            int col = (triangle.BottomRightVertex.X + triangle.TopLeftVertex.X ) / grid.Size;
            Debug.WriteLine(col);

            //If triangle is top right triangle, add one to col to get its Col / X value. 
            col = triangle.OuterVertex.X == triangle.BottomRightVertex.X ? col += 1 : col;

            return new GridValue(gridValueRow, col);
        }
    }
}
