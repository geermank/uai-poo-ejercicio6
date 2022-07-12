using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLibrary
{
    public class Habitacion
    {
        private string nombre;
        private string descripcion;
        private float precio;
        private int numero;
        private List<Artefacto> artefactos;
        private List<Caracteristica> caracteristicas;

        public Habitacion(string nombre, 
                          string descripcion, 
                          float precio, 
                          int numero, 
                          List<Artefacto> artefactos, 
                          List<Caracteristica> caracteristicas)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.numero = numero;
            this.artefactos = artefactos;
            this.caracteristicas = caracteristicas;
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public string Descripcion
        {
            get { return descripcion; }
        }

        public float Precio(int cantidadDeNoches)
        {
            float precioFinal = precio * cantidadDeNoches;
            caracteristicas.ForEach(caracteristica => precioFinal = caracteristica.AplicarRecargo(precioFinal));
            return precioFinal;
        }

        public int Numero { 
            get { return numero; } 
        }

        public List<Artefacto> Artefactos
        {
            get { return artefactos; }
        }

        public List<Caracteristica> Caracteristicas
        {
            get { return caracteristicas; }
        }
    }
}