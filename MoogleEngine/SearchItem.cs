namespace MoogleEngine;

public class SearchItem
{
    public SearchItem(string title, string snippet, float score)
    {
        this.Title = title;
        this.Snippet = snippet;
        this.Score = score;
    }

    public static List<SearchItem> SearchItemsList(List<float> Scores , string query)
    {   List<SearchItem> SearchItemsList = new List<SearchItem>();
        for (int i = 0; i < Scores.Count; i++)
        {   
            if(!float.IsNaN(Scores[i]) && (Scores[i] != 0)) 
            {  
                SearchItem si = new SearchItem( LoadDocuments.Titles()[i],LoadDocuments.ListOfSnippets(query)[i]+ Scores[i].ToString() , Scores[i]);
                SearchItemsList.Add(si);
            }

            SortByScoreDescending(SearchItemsList);
            
        }
        return SearchItemsList;
    }

     public static List<SearchItem> SortByScoreDescending(List<SearchItem> items)
    {
        return items.OrderByDescending(x => x.Score).ToList();
    }

  


    public string Title { get; private set; }

    public string Snippet { get; private set; }

    public float Score { get; private set; }
}
