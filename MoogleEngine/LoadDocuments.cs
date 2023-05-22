using System;
using System.Text.RegularExpressions;
namespace MoogleEngine;

public class LoadDocuments

{    
    public static Dictionary <string,decimal> idf = IDF(); 
    public static HashSet<string> Corpus = Vocabulary(); 
    

    
    


    /*Metodo load para cargar documentos que devuelve una lista de strings con cada documento en un string*/ 
    public static List<string> Load()
    {
        string[] directory = Directory.GetFiles(@"../Content" ,"*.txt");
        List<string> Documents = new List<string>();

        foreach (string document in directory)
        {
           
                Documents.Add(File.ReadAllText(document));
           
        }
       

        return Documents;
    }

     public static List<string> Normalize(string text)
    {    List<string> listWords = new List<string>();         
         char[] spliters = { ' ', '\n', '\t', ',', '.', ':', ';' };
         string[] words = text.Split(spliters); 
         string texto;
        
         foreach(string word in words)
         {             
                foreach (char c in word)
            {
                if (char.IsLetterOrDigit(c))
                {
                    texto = Regex.Replace(word, @"[^\w\s]", "");          
                    listWords.Add(texto.ToLower()); 
                    break;
                }
                                                                
            }
         }
     
      return listWords;     
    }

     public static List<List<string>> DocumetList()
    {   List<List<string>> DocumentList = new List<List<string>>();
        List <string> documentos = Load();
        foreach(string document in documentos)
        {    
            List<string> Document = (Normalize(document));
            DocumentList.Add(Document);
            
        }
      return DocumentList;  
    } 

    // Metodo que saca el vocabulario de todos los documentos 
 public static HashSet<string> Vocabulary ()
    {  HashSet<string> vocabulary = new HashSet<string>();
        int loadcount = Load().Count;
        List <string> documentos = Load();
              for (int i = 0; i < loadcount; i++)
              {
                    foreach(string word in Normalize(documentos[i]))
                    {
                        vocabulary.Add(word);
                    }
              }
                         
       
      return vocabulary;  
    } 

    public static  HashSet<string> DocumentVocabulary(string document) 
 {
    HashSet<string> vocabulary = new HashSet<string>();
    foreach(string word in Normalize(document))
    {
                        vocabulary.Add(word);
    }
     return vocabulary;   
 }

  public static List<HashSet<string>> AllDocumentVocabulary() 
 {  List<HashSet<string>> AllDocumentVocabulary  = new List<HashSet<string>>();
     List <string> documentos = Load();
   
    foreach(string document in documentos)
    {    
          AllDocumentVocabulary.Add(DocumentVocabulary(document));
    }
    return AllDocumentVocabulary;   
 }  
    


   
    
      

        public static Dictionary<string, decimal> IDF()
{
    Dictionary<string, decimal> idf = new Dictionary<string, decimal>();
   
    List<string> load = Load();
    int documentos = load.Count;
    HashSet <string> vocabulary = Vocabulary();

    foreach (string word in vocabulary)
    {
        idf.Add(word, 0);
    }

    for (int i = 0; i < load.Count; i++)
    {
        HashSet<string> palabrasTexto = new HashSet<string>(Normalize(load[i]));

        foreach (string palabra in vocabulary)
        {
            if (palabrasTexto.Contains(palabra))
            {
                idf.TryGetValue(palabra, out decimal value);
                idf[palabra] = value + 1;
            }
        }
    }

    foreach (KeyValuePair<string, decimal> entrada in idf)
    {
        decimal division = documentos / entrada.Value;
        if (division == 1)
        {
            division = (decimal)(1.001);
        }

        idf[entrada.Key] = (decimal)Math.Log10((double)(division));
    }

    return idf;
}

        

      public static List<Dictionary<string, decimal>> TF()
{
    List<Dictionary<string, decimal>> repeticiones = new List<Dictionary<string, decimal>>();

    foreach (List<string> texto in DocumetList())
    {   decimal wordcount = texto.Count;
        Dictionary<string, decimal> frecuencias = new Dictionary<string, decimal>();

        foreach (string palabra in texto)
        {
            if (frecuencias.ContainsKey(palabra))
            {
                frecuencias[palabra] += 1/ wordcount;
            }
            else
            {
                frecuencias.Add(palabra, 1/ wordcount);
            }
        }

        repeticiones.Add(frecuencias);
    }

    return repeticiones;
}

        

          
   
     

    

 public static List<Dictionary<string,decimal>> DictionaryTFIDF()
    { 
       List<Dictionary<string,decimal>> TFdic =  TF();

       Dictionary<string,decimal> IDFdic = idf ;
            
      
       List<HashSet<string>> Documents = AllDocumentVocabulary();

       int documnetCount = Load().Count;

      
       
      
      
       List<Dictionary<string, decimal>> values = new List<Dictionary<string, decimal>>();
      

          
             
             for (int i = 0; i < documnetCount ; i++)
            {   Dictionary<string,decimal> document= new Dictionary<string, decimal>();
                 foreach (string word in Corpus)
            {    
                if(Documents[i].Contains(word))  
                    {
                        document.Add(word,TFdic[i][word] * IDFdic[word]);
                    }
          
                else
                    {
                         document.Add(word,0);
                    }
           
           
               
            }
            
            values.Add(document);
        } 
        
     return values;
        
    }


     public static List<string> Titles()
{
    List<string> titles = new List<string>();
    string[] directory = Directory.GetFiles(@"../Content", "*.txt");
    foreach (string title in directory)
    {
        titles.Add(Path.GetFileName(title));
    }
    return titles;
}

    public static string GenerateSnippet(string word, string text)
{
    string snippet = "";
    int wordIndex = text.IndexOf(Normalize(word)[0]);
    if(wordIndex != -1)
    {
        int snippetStart = Math.Max(0, wordIndex - 20);
        int snippetEnd = Math.Min(text.Length, wordIndex + 20);
        int snippetLength = snippetEnd - snippetStart;
        if (snippetLength > 0)
        {
            snippet = text.Substring(snippetStart, snippetLength);
            if (snippetStart > 0) snippet = "..." + snippet;
            if (snippetEnd < text.Length) snippet += "...";
        }
    }
    return snippet;
}

 public static List<string> ListOfSnippets(string query)
{   List<string> ListOfSnippets = new List<string>();

    foreach(string document in Load())
    {
        ListOfSnippets.Add(GenerateSnippet(query,document));
    }
    return ListOfSnippets;
}



     

    


    // Metodo de calcula TF de una palabra en un documento

    public static Dictionary<string, decimal> TFOfDoc(List<string> wordList)
{
    Dictionary<string, decimal> tf = new Dictionary<string, decimal>();
    decimal wordCount = wordList.Count;

    foreach (string word in wordList)
    {
        if (tf.ContainsKey(word))
        {
            tf[word] += 1 / wordCount;
        }
        else
        {
            tf.Add(word, 1/ wordCount);
        }
    }

    return tf;
}






  

       public static Dictionary<string,decimal> QueryTFIDF(string query)
    {  
        Dictionary<string,decimal> QueryTFIDF = new Dictionary<string, decimal>();
      
        
         
       List<string> NormalizedQuery = Normalize(query);
        Dictionary<string,decimal> TFdic= TFOfDoc(NormalizedQuery);
       HashSet<string> VocabularyQuery = NormalizedQuery.ToHashSet();
         Dictionary<string,decimal> IDFdic = idf;

         foreach(string word in Corpus )
         {
            QueryTFIDF.Add(word,0);
         }

         foreach(string word in VocabularyQuery)
         {  if (Corpus.Contains(word))
         {
            QueryTFIDF[word] = TFdic[word] * IDFdic[word];
         }
            
         }
       
       
      
       return QueryTFIDF;

    }

   

            

    
}


    

 
