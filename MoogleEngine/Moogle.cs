﻿namespace MoogleEngine;


public static class Moogle
{   
      static Matrix DocumentMatrix = new Matrix(Matrix.MatrixVector);     
      public static SearchResult Query(string query) 
      {             
      
      Vector QueryVector = new Vector(LoadDocuments.ValueDictionary( LoadDocuments.Normalize(query)));
      
      List <float> Scores = Matrix.CalcularSimilitudesCoseno(QueryVector,DocumentMatrix);            
      

     
      SearchItem[] items =  SearchItem.SearchItemsList(Scores,query).ToArray();
                   
        return new SearchResult(items, query);
    }
}
