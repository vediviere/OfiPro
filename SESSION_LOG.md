=====================================
SESIÓN #1
Fecha: 2026-06-08
=================

Objetivo:

Validar la idea de negocio y definir el alcance inicial.

Completado:

✔ Se analizó la viabilidad del proyecto.
✔ Se eligió el modelo marketplace.
✔ Se identificó el problema de confianza.
✔ Se definió la propuesta de valor.
✔ Se identificaron riesgos del negocio.
✔ Se discutió el problema de acuerdos fuera de plataforma.
✔ Se decidió usar reputación como incentivo principal.

Decisiones importantes:

* El proyecto es viable.
* Se desarrollará.
* Se priorizará crecimiento y rentabilidad.
* Se trabajará por versiones cerradas.

Pendientes:

□ Definir arquitectura.
□ Definir entidades.
□ Crear solución.

Estado:

🟢 En tiempo.

=====================================
SESIÓN #2
Fecha: 2026-06-08
=================

Objetivo:

Construir la base técnica del proyecto.

Completado:

✔ Clean Architecture seleccionada.
✔ Monolito modular seleccionado.
✔ Solución creada.
✔ Proyectos creados:
- OfiPro.Api
- OfiPro.Application
- OfiPro.Domain
- OfiPro.Infrastructure

✔ Entidades principales creadas.
✔ Enums creados.
✔ ProjectRequirement implementado.
✔ ApplicationDbContext creado.
✔ EF Core configurado.
✔ OFIPRO_MASTER.md creado.
✔ SESSION_LOG.md creado.
✔ Repositorio GitHub creado.
✔ Primer commit realizado.
✔ Estructura física corregida.

Problemas encontrados:

1. EF Core 10 incompatible con .NET 8.
   Solución:
   Migrar a EF Core 8.0.27.

2. HasColumnType no reconocido.
   Solución:
   Agregar referencias EF Core faltantes.

3. Git inicializado en carpeta incorrecta.
   Solución:
   Reinicializar Git en la raíz real del proyecto.

Decisiones importantes:

* No usar microservicios.
* Todos los usuarios pueden publicar proyectos.
* ProfessionalProfile tendrá una sola especialidad.
* Se utilizará ProjectRequirement para múltiples necesidades.
* Se utilizará documentación viva.

Pendientes:

□ RatingConfiguration.
□ InvitationConfiguration.
□ InitialCreate.
□ Base de datos.
□ JWT.
□ Usuarios.
□ Proyectos.
□ Propuestas.

Estado:

🟢 En tiempo.

=====================================
SESIÓN #3
Fecha: 2026-06-09
=================

Objetivo:

Completar el Bloque 1 (Fundación) y generar la primera base de datos funcional.

Completado:

✔ ProjectRequirementConfiguration.
✔ ProjectPhotoConfiguration.
✔ ProposalConfiguration.
✔ EvidenceConfiguration.
✔ RatingConfiguration.
✔ InvitationConfiguration.

✔ Todas las configuraciones EF completadas.

✔ Migración creada:
InitialCreate

✔ Snapshot generado.

✔ Base de datos creada:
OfiProDb

✔ Tablas generadas correctamente en SQL Server.

✔ Repositorio GitHub configurado correctamente.

✔ Se validó que el Bloque 1 quedó terminado.

Problemas encontrados:

1. Add-Migration no reconocido.
   Causa:
   Uso de Developer PowerShell.
   Solución:
   Utilizar la Consola del Administrador de paquetes.

2. RatingConfiguration e InvitationConfiguration utilizaban propiedades inexistentes.
   Causa:
   Nombres no alineados con Domain.
   Solución:
   Corregir utilizando las propiedades reales de las entidades.

3. Git inicializado fuera de la raíz del proyecto.
   Solución:
   Reinicializar repositorio en la carpeta correcta.

Decisiones importantes:

* Validar requisitos antes de ejecutar comandos.
* Validar nombres contra Domain antes de generar configuraciones.
* Cerrar bloques formalmente antes de iniciar otros.

Resultado:

BLOQUE 1 - FUNDACIÓN
COMPLETADO ✅

Pendientes:

□ Commit final del Bloque 1.
□ Iniciar Bloque 2 - Auth.

Estado:

🟢 En tiempo.

# =====================================
# SESIÓN 2026-06-12

## Objetivo

Completar Bloque 2 - Auth.
# =====================================

## Completado

- Se creó JwtSettings.
- Se configuró JWT en appsettings.json.
- Se implementó IJwtService.
- Se implementó JwtService.
- Se configuró Authentication.
- Se configuró Authorization.
- Se creó IUserRepository.
- Se implementó UserRepository.
- Se implementó AuthService.
- Se creó AuthController.
- Se implementó Register.
- Se implementó Login.
- Se configuró Swagger Authorize.
- Se creó TestController protegido.

## Pruebas realizadas

### Register

Resultado:

200 OK

### Login

Resultado:

200 OK

### Endpoint protegido

GET /api/Test/secure

Resultado:

200 OK

## Decisiones tomadas

- JWT contendrá únicamente:
  - UserId
  - Email
  - Role

- Todo usuario nuevo se registra como Cliente.

- User tendrá colección UserRoles.

- BCrypt se implementa en Infrastructure.

- Repository Pattern será obligatorio para acceso a datos.

## Estado del proyecto

Bloque 1: Completo

Bloque 2: Completo

Bloque 3: Pendiente

## Próximo paso

Iniciar Bloque 3 - Usuarios.


# =====================================
# SESIÓN 2026-06-14

## Objetivo

Completar Bloque 2 (Auth) y Bloque 3 (Usuarios).

# =====================================

## Auth

Completado:

* JwtSettings
* IJwtService
* JwtService
* IAuthService
* AuthService
* IUserRepository
* UserRepository
* AuthController
* Register
* Login
* JWT Authentication
* JWT Authorization
* Swagger Authorize

Pruebas:

* POST /api/auth/register → 200
* POST /api/auth/login → 200
* GET /api/test/secure → 200

Resultado:

JWT funcionando correctamente.

## Usuarios

Completado:

* UserProfileDto
* UpdateUserProfileDto
* IUserService
* UserService
* GET /api/users/profile
* PUT /api/users/profile

Pruebas:

* GET profile → 200
* PUT profile → 200

## Administración

Completado:

* GET /api/users
* GET /api/users/{id}
* PATCH activate
* PATCH deactivate
* DELETE soft delete

Pruebas:

* GET /api/users → 200

## Seeder

Completado:

* ApplicationDbSeeder
* Admin automático

Usuario:

[admin@ofipro.com](mailto:admin@ofipro.com)

## Decisiones

D013

El primer administrador se crea mediante seed automático al iniciar la aplicación.

Razón:

* Facilita pruebas.
* Evita inserciones manuales.
* Reutilizable para futuros catálogos.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Pendiente

## Próximo paso

Iniciar Bloque 4 - Proyectos.

# =====================================

# SESIÓN 2026-06-15

## Objetivo

Completar Bloque 4 - Proyectos.

# =====================================

## Seeder

Completado:

* SeedCategoriesAsync
* Categorías iniciales
* Subcategorías iniciales

## Repositorio

Completado:

* IProjectRepository
* ProjectRepository

Métodos:

* GetByIdAsync
* GetAllAsync
* GetByUserIdAsync
* AddAsync
* UpdateAsync
* UpdateRequirementsAsync

## Servicios

Completado:

* IProjectService
* ProjectService

Casos de uso:

* CreateAsync
* GetByIdAsync
* GetAllAsync
* GetMyProjectsAsync
* UpdateAsync
* UpdateRequirementsAsync
* DeleteAsync

## Controller

Completado:

* ProjectsController

Endpoints:

* POST /api/projects
* GET /api/projects
* GET /api/projects/my-projects
* GET /api/projects/{id}
* PUT /api/projects/{id}
* PUT /api/projects/{id}/requirements
* DELETE /api/projects/{id}

## Pruebas

POST /api/projects → 200

GET /api/projects → 200

GET /api/projects/my-projects → 200

GET /api/projects/{id} → 200

PUT /api/projects/{id} → 200

PUT /api/projects/{id}/requirements → 200

DELETE /api/projects/{id} → 200

## Decisiones

D014

ProjectRequirements se administran mediante endpoint independiente.

Razón:

* Responsabilidad separada.
* Arquitectura consistente.
* Mejor escalabilidad.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Pendiente

## Próximo paso

Iniciar Bloque 5 - Propuestas.


# =====================================

# SESIÓN 2026-06-16

## Objetivo

Completar Bloque 5 - Propuestas.

# =====================================

## Corrección de modelo

Completado:

* Proposal ahora se relaciona con ProjectRequirement.
* Se eliminó la relación obsoleta Proposal → Project.
* Se agregó navegación ProjectRequirement → Proposals.
* Se generó migración correctiva.

Problema detectado:

* FK_Proposals_Projects_ProjectId seguía existiendo.

Solución:

* Eliminar ProjectId de Proposal.
* Corregir ProposalConfiguration.
* Crear migración RemoveOldProjectRelationFromProposal.

## DTOs

Completado:

* CreateProposalDto
* UpdateProposalDto
* ProposalDto

## Repositorio

Completado:

* IProposalRepository
* ProposalRepository

## Servicio

Completado:

* IProposalService
* ProposalService

Reglas implementadas:

* Un contratista solo puede tener una propuesta activa por ProjectRequirement.
* Una propuesta retirada puede volver a crearse.
* Solo propuestas pendientes pueden modificarse.
* Solo propuestas pendientes pueden retirarse.

## Controller

Completado:

* ProposalsController

Endpoints:

* POST /api/proposals
* PUT /api/proposals/{id}
* GET /api/proposals/my-proposals
* GET /api/proposals/{id}
* GET /api/proposals/requirement/{id}
* PATCH /api/proposals/{id}/accept
* PATCH /api/proposals/{id}/reject
* PATCH /api/proposals/{id}/withdraw

## Pruebas

POST /api/proposals → 200

GET /api/proposals/my-proposals → 200

GET /api/proposals/{id} → 200

PUT /api/proposals/{id} → 200

PATCH withdraw → 200

PATCH accept → 200

PATCH reject → 200

## Regla D016 validada

Prueba realizada:

* Contratista A crea propuesta.
* Contratista B crea propuesta.
* Cliente acepta propuesta A.

Resultado:

* Propuesta A → Aceptada.
* Propuesta B → Rechazada.

Funcionamiento correcto.

## Refactor técnico

Completado:

* ProposalRepository.UpdateAsync → SaveChangesAsync
* ProjectRepository.UpdateAsync → SaveChangesAsync

Se conserva:

* UserRepository.UpdateAsync(User user)

porque realiza una actualización explícita.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 6 - Contrataciones → Pendiente

## Próximo paso

Iniciar Bloque 6 - Contrataciones.


# =====================================

# SESIÓN 2026-06-17

## Objetivo

Completar Bloque 5.5 - Seguridad y Calidad Base.

# =====================================

## Seguridad JWT

Completado:

* JWT Key removida de appsettings.json.
* JWT Key configurada mediante User Secrets.
* Login validado después del cambio.

## Autorización de propuestas

Completado:

* AcceptAsync ahora recibe el UserId del usuario autenticado.
* RejectAsync ahora recibe el UserId del usuario autenticado.
* Se valida Project.CreatedByUserId contra el UserId del token.
* Solo el propietario del proyecto puede aceptar propuestas.
* Solo el propietario del proyecto puede rechazar propuestas.

## Validaciones DTO

Completado:

* Validaciones agregadas en CreateProjectDto.
* Validaciones agregadas en CreateProjectRequirementDto.
* Validaciones agregadas en CreateProposalDto.
* Se validan textos requeridos.
* Se validan longitudes máximas.
* Se valida que Price no acepte valores negativos o cero.

## Excepciones

Completado:

* NotFoundException.
* ForbiddenException.
* BadRequestException.
* ExceptionMiddleware.
* Registro del middleware en Program.cs.

## Pruebas

JWT:

* Login → 200

Validaciones:

* Propuesta con precio negativo → 400

Excepciones:

* Propuesta inexistente → 404
* Propuesta no pendiente → 400
* Usuario no propietario intentando rechazar propuesta → 403

Autorización:

* Usuario no propietario no pudo aceptar/rechazar propuesta.
* Usuario propietario sí pudo aceptar/rechazar propuesta.

## Decisiones

D018

Los secretos no deben guardarse en appsettings.json ni en el repositorio.

D019

Aceptar o rechazar propuestas requiere validar al propietario del proyecto.

D020

Las pruebas que requieran IDs se apoyarán en consultas directas a base de datos.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API.

## Próximo paso

Iniciar Bloque 5.6 - Limpieza de Consistencia API.

# =====================================

# SESIÓN 2026-06-17 (PARTE 2)

## Objetivo

Completar Bloque 5.6 - Limpieza de Consistencia API.

# =====================================

## Excepciones

Completado:

* Reemplazo de Exception genérica en servicios.
* Implementación consistente de:
  * BadRequestException
  * ForbiddenException
  * NotFoundException

Pruebas:

* Login inválido → 400
* Proyecto inexistente → 404
* Proyecto ajeno → 403
* Propuesta ajena → 403

## AuthController

Completado:

* Eliminación de try/catch redundantes.
* Delegación total al ExceptionMiddleware.

Resultado:

Formato estándar unificado en toda la API.

## DTOs Update

Completado:

* UpdateProjectDto
* UpdateProjectRequirementsDto
* UpdateProposalDto
* UpdateUserProfileDto

Pruebas:

* Título vacío → 400
* Precio negativo → 400

## Soft Delete

Validado:

* ProjectRepository.GetAllAsync ya excluye DeletedAt != null.

No se requirió cambio.

## JWT

Completado:

* Expiración sincronizada entre configuración y respuesta de login.

## TestController

Completado:

* Restricción a rol Administrador.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Pendiente

## Próximo paso

Iniciar Bloque 6 - Contrataciones.