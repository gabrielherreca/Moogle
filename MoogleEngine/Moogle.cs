namespace MoogleEngine;


public static class Moogle
{   
      static Matrix DocumentMatrix = new Matrix(Matrix.ListForMatrix());     
      public static SearchResult Query(string query) 
      {             
      
      Vector QueryVector = new Vector(LoadDocuments.QueryTFIDF(query));
      
      float[] Scores = Matrix.CalcularSimilitudesCoseno(QueryVector,DocumentMatrix);            
      

     
      SearchItem[] items =  SearchItem.SortByScoreDescending(SearchItem.SearchItemsList(Scores,query)).ToArray();
                   
        return new SearchResult(items, query);
    }
}
