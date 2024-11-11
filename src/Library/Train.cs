//--------------------------------------------------------------------------------
// <copyright file="Train.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace ClassLibrary;

/// <summary>
/// Esta clase representa un tren muy básico.
/// </summary>
public class Train
{
    /// <summary>
    /// Obtiene un valor que indica si las maquinas del tren han sido encendidas o no.
    /// </summary>
    /// <value><c>true</c> si las máquinas fueron encendidas, <c>false</c> en caso contrario.</value>
    public bool IsEngineStarted { get; private set; }

    /// <summary>
    /// Enciende las máquinas del tren.
    /// </summary>
    /// <returns>
    /// <c>true</c> si las máquinas pueden ser encendidas, <c>false</c> en caso contrario.
    /// </returns>
    public bool StartEngines()
    {
        if (this.IsEngineStarted)
        {
            Console.WriteLine("The engines are already running");
            return false;
        }

        this.IsEngineStarted = true;
        Console.WriteLine("Engines on");
        return true;
    }

    /// <summary>
    /// Detiene las máquinas del tren.
    /// </summary>
    /// <returns>
    /// <c>true</c> si las máquinas pueden ser detenidas, <c>false</c> en caso contrario.
    /// </returns>
    public bool StopEngines()
    {
        if (this.IsEngineStarted)
        {
            this.IsEngineStarted = false;
            Console.WriteLine("Engines off");
            return true;
        }

        Console.WriteLine("The engines are already stopped");
        return this.IsEngineStarted;
    }
}
