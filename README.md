# CimaCheck

Sistema de registro de asistentes para eventos de la UABC (Universidad Autónoma de Baja California).

## Descripción

CimaCheck es una aplicación de escritorio desarrollada en WPF (.NET 10) que permite gestionar el registro de asistentes a eventos universitarios. Soporta tres tipos de registro:

- **Individual**: Para asistentes que registran su asistencia de forma individual
- **Grupo**: Para registrar múltiples asistentes simultáneamente
- **Cimarrón**: Para estudiantes cimarrones de la UABC

## Tecnologías

- **Framework**: .NET 10 (WPF)
- **Backend**: Supabase (PostgreSQL)
- **Librerías**:
  - Microsoft.Extensions.Configuration
  - Supabase (supabase-csharp)
  - postgrest-csharp

## Estructura del Proyecto

```
CimaCheck/
├── Controllers/          # Controladores de lógica de negocio
│   ├── AlumnosController.cs
│   ├── SchoolsController.cs
│   ├── UniversitiesController.cs
│   ├── JsonController.cs
│   └── SupabaseSettings.cs
├── Models/              # Modelos de datos
│   ├── Alumno.cs
│   ├── Cimarron.cs
│   ├── Carrera.cs
│   ├── Escuela.cs
│   ├── Facultad.cs
│   ├── Company.cs
│   └── Externos.cs
├── Pages/                # Páginas de la interfaz
│   ├── PagePrincipal.xaml
│   ├── IndividualRegistration.xaml
│   ├── GroupRegistration.xaml
│   └── CimaRegistration.xaml
├── Resources/Icons/      # Iconos e imágenes
├── Services/            # Servicios adicionales
│   └── DataManager.cs
├── App.xaml             # Configuración de la aplicación
├── MainWindow.xaml      # Ventana principal
└── appsettings.json    # Configuración (URL de Supabase)
```

## Requisitos

- .NET 10 SDK
- Windows 10/11
- Conexión a Internet (para acceder a Supabase)

## Configuración

El archivo `appsettings.json` contiene la configuración de Supabase:

```json
{
  "Supabase": {
    "Url": "https://tu-proyecto.supabase.co",
    "Key": "tu-api-key"
  },
  "AppSettings": {
    "AppName": "Registro de carnets",
    "Version": "1.0.0.0"
  }
}
```

## Ejecución

```bash
cd CimaCheck
dotnet run
```

O abre el proyecto en Visual Studio y presiona F5.

## Compilación

```bash
dotnet build -c Release
```

El ejecutable se generará en `CimaCheck/bin/Release/net10.0-windows/`.

## Características

- Interfaz gráfica intuitiva con diseño UABC
- Registro de asistencia en tiempo real
- Soporte para múltiples tipos de asistentes (estudiantes, externos, cimarrones)
- Integración con base de datos en la nube
- Validación de datos

## Licencia

Todos los derechos reservados © 2025 UABC.
