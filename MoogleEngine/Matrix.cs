using System;
using System.Collections.Generic;
using System.Linq;

namespace MoogleEngine;

public class Matrix
{
    private List<Vector> valores;


    public Matrix(List <Vector> valores)
    {
        this.valores = valores;
    }

     public List<Vector> GetVectores()
    {
        return valores;
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

   

    public static float[] CalcularSimilitudesCoseno(Vector vector, Matrix matrix)
{
    List<Vector> vectores = matrix.GetVectores();
    float[] similitudes = new float[vectores.Count];

    for (int i = 0; i < similitudes.Length; i++)
    {
        Vector otroVector = vectores[i];
        float similitud = vector.CalculateSimilitudCoseno(otroVector);
        similitudes[i] = similitud;
    }

    return similitudes;
}

   

}



        

 

