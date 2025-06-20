Console.WriteLine("2525 - ESTRUCTURA DE DATOS - UEA / SEMANA 04");
Console.WriteLine("Ejemplo: Agenda de servicios de un taller mecánico");

// Crear la agenda con una capacidad de 10 servicios
AgendaServicios agenda = new AgendaServicios(10);

// Agregar servicios a la agenda
Mantenimiento mantenimiento1 = new Mantenimiento("Juan Mena", "Toyota Corolla", "25/06/2025 11:00", "Cambio de aceite");
Mantenimiento mantenimiento2 = new Mantenimiento("Jair Cordova", "Toyota Rav 4", "25/06/2025 11:00", "Cambio de aceite");
Mantenimiento mantenimiento3 = new Mantenimiento("Beatriz Ruiz", "Kia Rio", "25/06/2025 12:00", "Revision de frenos");

Reparacion reparacion1 = new Reparacion("Miguel Castro", "Chevrolet Onix", "25/06/2025 10:30", "Reparacion del alternador");
Reparacion reparacion2 = new Reparacion("Sonia Guerrero", "Chevrolet Aveo", "25/06/2025 14:30", "Reparacion del radiador");
Reparacion reparacion3 = new Reparacion("David Freire", "Toyota 4Runner", "25/06/2025 14:30", "Reparacion del alternador");
Reparacion reparacion4 = new Reparacion("Mayra Moya", "Kia Sportage", "25/06/2025 16:30", "Reparacion del motor");

agenda.AgregarServicio(mantenimiento1);
agenda.AgregarServicio(mantenimiento2);
agenda.AgregarServicio(mantenimiento3);
agenda.AgregarServicio(reparacion1);
agenda.AgregarServicio(reparacion2);
agenda.AgregarServicio(reparacion3);
agenda.AgregarServicio(reparacion4);

// Ordenar por nombre del cliente
agenda.OrdenarPorCliente();

// Mostrar todos los servicios
agenda.MostrarServicios();