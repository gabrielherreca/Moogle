using System;
using System.Text.RegularExpressions;
namespace MoogleEngine;

public class LoadDocuments

{   //Devueve cada texto en en string
    public static List <string> LoadList = Load();

    //Devulve la cantidad de documntos
    public static int DocumentCount = LoadList.Count;

    //Devuelve la ruta de cada documento

    public static List <string> TitleOfDocuments = Titles();

    // Devuelve el vocabulario entre todos los documentos

    public static List<string> Corpus = Vocabulary();

    //Devulve cada documento normalizado

    public static List<List<string>> NormalizedDocuments = DocumetList();

    //Devuelve una lista de diccionarios con los tf idf

    public static List<Dictionary<string,decimal>> DictionaryMatrix = DictionaryTFIDF();

    // Diccionario con el idf de cada palabra Vocabulario
    public static Dictionary<string,decimal> CorpusIDF = VocabularyIDF(); 


    
    

    /*Metodo load para cargar documentos que devuelve una lista de strings con cada documento en un string*/ 
    public static List<string> Load()
    {
        string[] directory = Directory.GetFiles(@"..\Content" ,"*.txt");
        List<string> Documents = new List<string>();

        foreach (string document in directory)
        {
           
                Documents.Add(File.ReadAllText(document));
           
        }
       

        return Documents;
    }

    public static List <string> Titles()
    {   List<string> titles = new List<string>();
        string[] directory = Directory.GetFiles(@"..\Content","*.txt");
        foreach(string title in directory)
        {
            titles.Add(title);
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

    foreach(string document in LoadList)
    {
        ListOfSnippets.Add(GenerateSnippet(query,document));
    }
    return ListOfSnippets;
}



     /*Metodo que por documento , elimina caracteres especiales y divide el texto en palabras y las devuelve en una lista de string*/
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
    
    // Metodo de calcula TF de una palabra en un documento
      public static decimal WordTF(string word , List<string> text)
    {   decimal tf = 0 ;
        foreach (string wordintext in text )
        {
            if (word == wordintext)
            {
                tf += 1;
            }
        }
        tf = tf /text.Count;
        return tf; 
    }
    // Metodo que calcula IDF de una plabra
    public static decimal WordIdf(string word)
    {   decimal idf = 0;
       for (int i = 0; i < DocumentCount; i++)
       {
            for ( int j = 0; j < NormalizedDocuments[i].Count; j ++)
            {
                if (NormalizedDocuments[i].Contains(word))
                {
                  idf += 1;
                  break;
                }
            }
       }
      idf =(decimal) Math.Log10((double)(DocumentCount/ idf));
        return idf;
    }
    // Metodo que calcula TF IDF de una palabra 
     public static decimal WordTfIdf(string word ,  List<string> text)
     {  
        decimal wordtfidf = WordTF(word, text)* CorpusIDF[word];
        return wordtfidf;
     }

     public static Dictionary<string,decimal> VocabularyIDF()
     {   Dictionary<string,decimal> VocabularyIDF = new Dictionary<string, decimal>();
        foreach(string word in Corpus)
        {
            VocabularyIDF.Add(word,WordIdf(word));
        }
       return VocabularyIDF; 
     }

    //Metodo que crea un diccionario para un documento con cada palabra y su valor de tf idf asociado    
     public static Dictionary<string,decimal> ValueDictionary(List<string> list)
    {
        Dictionary< string, decimal> values = new Dictionary<string, decimal>();
        foreach ( string word in Corpus)
        {
            if (list.Contains(word))
            {
                values.Add(word, WordIdf(word));
                
            }
            else
            {
                values.Add(word,0);
            }
        }
      return values;  
    }


    //Devuelve una lista de listas de string con cada documento normalizado en una lista  
    public static List<List<string>> DocumetList()
    {   List<List<string>> DocumentList = new List<List<string>>();
        foreach(string document in Load())
        {   List<string> Document = new List<string>();
            Document = (Normalize(document));
            DocumentList.Add(Document);
            
        }
      return DocumentList;  
    }

    /* Devuelve una lista de diccionarios .Cada diccionario representa un 
    docuemnto con cada palabra y su valor de tf idf asociado */   
     public static List<Dictionary<string,decimal>> DictionaryTFIDF()
    {   List<Dictionary<string,decimal>> DictionaryTFIDF = new List<Dictionary<string,decimal>>();
        foreach(List<string> list in NormalizedDocuments)
        {   
            DictionaryTFIDF.Add(ValueDictionary(list));
        }

     return DictionaryTFIDF;
    }



    
    // Metodo que devuelve una lista normalizada de todas las palabras de lños documentos incluyendo repeticiones
     public static List<string> NormalizeAll()
    {   List<string> NormalizeAll = new List<string>();
        for (int i = 0; i < Load().Count; i++)
        {
            foreach (string word in Normalize(Load()[i]))
            {
                NormalizeAll.Add(word);
            }
        }
       return NormalizeAll; 
    }
    // Metodo que saca el vocabulario de todos los documentos 
 public static List<string> Vocabulary ()
    {   List<string> vocabulary = new List<string>();
        
            foreach( string word in NormalizeAll())
            {
                if (!vocabulary.Contains(word))
                {
                    vocabulary.Add(word);
                }
            }
       
      return vocabulary;  
    }   
}