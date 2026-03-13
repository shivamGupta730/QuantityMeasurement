using System;

using ModelLayer.Entity;
using ModelLayer;
using BusinessLayer.Services;
using ApplicationLayer.Interface;


namespace ApplicationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            IQuantityMeasurementService service = new QuantityMeasurementService();
            IMainMenu menu = new MainMenu(service);
            menu.ShowMainMenu();
        }
    }
}


