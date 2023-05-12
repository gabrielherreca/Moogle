using System;
using System.Collections.Generic;
using System.Linq;

namespace MoogleEngine;

public class Matrix
{
    private List<Vector> valores;

    public static List<Vector> MatrixVector = ListForMatrix();

    public Matrix(List <Vector> valores)
    {
        this.valores = valores;
    }

public static List<Vector> ListForMatrix()
    { List<Vector> Matrix = new List<Vector>();
        {
            foreach(Dictionary<string,decimal> dictionary in LoadDocuments.DictionaryTFIDF() )
            {   Vector v = new Vector(dictionary);
                Matrix.Add(v);
            }
        }
     return Matrix;
    }

   
      public static  List<float> CalcularSimilitudesCoseno(Vector vector , Matrix matrix)
    {
        List<float> similitudes = new List<float>();

        foreach(Vector otroVector in matrix.valores) 
        
           {
                float similitud = vector.CalculateSimilitudCoseno(otroVector);
                 similitudes.Add(similitud);
           } 
        

        return similitudes;
    }
}



        

 

