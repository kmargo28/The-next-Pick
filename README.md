# The Next Pick

Simulador de carrera NBA en C# y Blazor WebAssembly para Programación III.

## Ejecutar

Requisitos: SDK .NET 10, navegador actualizado y conexión a Internet para restaurar paquetes. Algunas imágenes se cargan de servicios externos. La partida se mantiene en memoria y se reinicia al recargar la página.

Desde la carpeta que contiene `The-next-Pick.csproj`:

```powershell
dotnet restore The-next-Pick.csproj
dotnet watch --project The-next-Pick.csproj run --launch-profile http
```

Abrir http://localhost:5194. Detener con Ctrl+C. No requiere base de datos ni ruta D:.

## Verificar

```powershell
dotnet build The-next-Pick.csproj
dotnet run --project Validation/Validation.csproj
```

Validation es un ejecutable de comprobaciones sin dependencias de pruebas externas. Termina con error si una comprobación falla. Valida arquetipos, Observer, ambos ritmos, 20 temporadas de 82 partidos, premios, lesiones, draft y recompensas.

## Patrones y reglas

- Strategy: `Engine/Strategies/`, selección de arquetipo en `Pages/Home.razor` y delegación de simulación en `Engine/GameStateService.cs`.
- Observer: `Engine/Observer/`; GameStateService notifica a Home, que vuelve a representar sus componentes hijos.
- La temporada regular tiene seis bloques de 14, 14, 13, 14, 14 y 13 partidos. El All-Star ocurre al completar 41. Se necesitan 42 victorias para acceder a playoffs.
- El desgaste de energía se aplica una vez por bloque: 10 en detallada y 7 en rápida. Las estadísticas se generan y acumulan por partido.
- Los playoffs son cuatro rondas arcade de eliminación; no simulan series al mejor de siete.
- El catálogo contiene 52 registros con ID único; algunos comparten contexto y opciones. Las probabilidades base por rareza son 75/15/8/2 y pueden cambiar al agotarse candidatos elegibles.
- Las lesiones restan apariciones del jugador, pero el equipo completa su calendario. La lesión grave termina la temporada sin playoffs; el jugador se recupera para la siguiente.


