# Prueba técnica

**Requerimientos**
- NET Core Runtime 2.1.1
- NET Core SDK 2.1.1
- Postman (Opcional)

**Instalación**

Ejecutamos los siguientes comandos en el directorio del proyecto: 
`{yourdirectory}\backend\`

```
dotnet restore
dotnet run
```

La aplicación se ejecuta en el puerto 5100:
http://localhost:5100/api/driver

**Base de datos**

Se utilizo SQLlite como motor de base de datos, ya que no requiere instalar dependencias.
La base de datos ya viene precargada con información de prueba en cada una de las tablas.

En caso de que se requiera una instalación limpia, simplemente eliminamos el siguiente archivo de base de datos.

```
{yourdirectory}\backend\database.db
```

Luego ejecutamos los siguientes comandos dentro el directorio del proyecto para recrear la base de datos.

```
dotnet ef migrations add DbSchema
dotnet ef database update
```

**Ejecución**

En el directorio del proyecto hay una carpeta llamada "Postman", la cual contiene un archivo JSON listo para importar en Postman; este incluye cada uno de los request hacía la API con su respectivo Payload.

```
{yourdirectory}\backend\Postman
```

De igual forma se puede importar desde el siguiente enlace:
https://www.getpostman.com/collections/8827eb34efa8ecb79896

**Documentación**

Se puede revisar facilmente la documentación de la API a través de Swagger. Solo es necesario acceder a la siguiente url:

```
http://localhost:5100/swagger
```