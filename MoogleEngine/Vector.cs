
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoogleEngine;

public class Vector
{
    private Dictionary<string, decimal> valores;

    public Vector( Dictionary<string, decimal> valores)
    {
        this.valores = valores;
    }

    public float CalcularProductoPunto(Vector otroVector)
    {
        float productoPunto = 0;
        foreach (var clave in valores.Keys)
        {
            if (otroVector.valores.ContainsKey(clave))
            {
                productoPunto += (float) (valores[clave] * otroVector.valores[clave]);
            }
        }
        return productoPunto;
    }

    public decimal CalcularProductoMagnitud(Vector otroVector)
    {
        decimal productoMagnitud = CalcularMagnitud() * otroVector.CalcularMagnitud();
        return productoMagnitud;
    }

    public decimal CalcularMagnitud()
    {
        decimal sumaCuadrados = 0;
        var values = this.valores;
        foreach (var valor in values)
        {
            sumaCuadrados += valor.Value * valor.Value;
        }
        return (decimal) Math.Sqrt((double) sumaCuadrados);
    }
    
   
    public float CalculateSimilitudCoseno(Vector otroVector)
    {   
        float ScalarProduct = CalcularProductoPunto(otroVector);
        float Magnitud = (float) CalcularProductoMagnitud(otroVector);
        float similarity = ScalarProduct/Magnitud;
        return similarity;
    }

    
    
   

    

    
}  

