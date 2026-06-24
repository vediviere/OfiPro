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

# =====================================

# SESIÓN 2026-06-18

## Objetivo

Iniciar y completar funcionalmente Bloque 6 - Contrataciones.

# =====================================

## Núcleo de contratación

Completado:

* ContractStatus.
* Entidad Contract.
* ContractConfiguration.
* Tabla Contracts en SQL Server.
* IContractRepository.
* ContractRepository.
* IContractService.
* ContractService.
* ContractsController.

## Flujo automático

Completado:

* Al aceptar una propuesta se crea automáticamente una contratación.
* La contratación inicia en estado PendienteInicio.
* Se guarda ProposalId.
* Se guarda ProjectRequirementId.
* Se guarda ClientUserId.
* Se guarda ContractorUserId.
* Se guarda AgreedPrice.
* Se guarda EstimatedTime.

## Endpoints creados

* GET /api/contracts/mine
* GET /api/contracts/{id}
* PATCH /api/contracts/{id}/status

## Estados implementados

* PendienteInicio
* EnProceso
* PendienteConfirmacion
* Finalizado
* Cancelado

## Reglas implementadas

* Solo el contratista puede iniciar una contratación.
* Solo el contratista puede marcar una contratación como pendiente de confirmación.
* Solo el cliente puede finalizar una contratación.
* Cliente o contratista pueden cancelar una contratación activa.
* Una contratación finalizada no puede cambiar de estado.
* Una contratación cancelada no puede cambiar de estado.

## Pruebas realizadas

* Aceptar propuesta crea contratación automáticamente → 200
* GET /api/contracts/mine → 200
* GET /api/contracts/{id} → 200
* PendienteInicio → EnProceso → 200
* EnProceso → PendienteConfirmacion → 200
* PendienteConfirmacion → Finalizado → 200
* Cancelación de contratación → 200
* Intentar mover contratación cancelada → 400

## Decisiones

D024

Los DTOs de respuesta deben usar identificadores descriptivos para facilitar pruebas y consumo desde frontend.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo funcionalmente

## Pendiente técnico

Realizar Bloque 6.8 - Refactor de nombres descriptivos en DTOs.

## Próximo paso

Iniciar Bloque 6.8 - Refactor de nombres descriptivos en DTOs antes de comenzar Bloque 7 - Evidencias.

# =====================================

# SESIÓN 2026-06-19

## Objetivo

Cerrar pendientes derivados del Bloque 6 antes de iniciar Bloque 7 - Evidencias.

# =====================================

## Bloque 6.8 - Refactor de nombres descriptivos en DTOs

Completado:

* UserProfileDto cambió Id por UserId.
* ProjectDto cambió Id por ProjectId.
* ProjectRequirementDto cambió Id por ProjectRequirementId.
* ProposalDto cambió Id por ProposalId.
* ContractDto cambió Id por ContractId.
* Se actualizaron los mapeos correspondientes.

Pruebas:

* GET /api/projects → 200
* GET /api/proposals/my-proposals → 200
* GET /api/contracts/mine → 200
* GET /api/users/profile → 200

Resultado:

Las respuestas de API ahora son más claras y fáciles de depurar.

## Bloque 6.9 - Flujo mínimo de Contratista

Completado:

* Se agregaron métodos HasRoleAsync y AddRoleAsync en UserRepository.
* Se agregó validación de rol Contratista en ProposalService.CreateAsync.
* Se creó endpoint PATCH /api/users/activate-contractor.
* Un usuario Cliente puede activar también el rol Contratista.
* Se validó que solo contratistas puedan enviar propuestas.

Pruebas:

* Cliente sin rol Contratista intentando crear propuesta → 403
* Usuario con rol Contratista intentando crear propuesta → pasa validación de rol
* Activación de contratista → 200
* Verificación en BD muestra Role 1 y Role 2 para el usuario activado

Resultado:

El flujo mínimo Cliente → Contratista quedó implementado.

## Bloque 6.10 - Orden de interfaces Application

Completado:

* Interfaces de repositorios movidas a Interfaces/Repositories.
* Interfaces de servicios movidas a Interfaces/Services.
* Visual Studio actualizó namespaces y referencias.
* La solución compiló correctamente.
* Pruebas básicas posteriores correctas.

Resultado:

La capa Application queda más ordenada antes de continuar con nuevos módulos.

## Estado

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

## Pendiente

Iniciar Bloque 7 - Evidencias.

## Próximo paso

Crear el módulo de Evidencias asociado a contrataciones.

# =====================================

# SESIÓN 2026-06-20

## Objetivo

Resolver diagnóstico posterior al Bloque 6 antes de iniciar Bloque 7 - Evidencias.

# =====================================

## Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7

Completado:

* Se revisó diagnóstico del estado actual del backend.
* Se identificó un filtro redundante en UserService.GetAllAsync.
* Se eliminó el filtro DeletedAt == null del servicio porque el repositorio ya filtra usuarios eliminados.
* Se detectó que GET /api/proposals/requirement/{projectRequirementId} permitía consultar propuestas a cualquier usuario autenticado.
* Se decidió que solo el dueño del proyecto puede consultar las propuestas de un requerimiento.
* Se agregó validación de propietario antes de devolver propuestas.
* Se agregó GetProjectRequirementOwnerUserIdAsync en IProposalRepository.
* Se implementó GetProjectRequirementOwnerUserIdAsync en ProposalRepository.
* Se actualizó ProposalService.GetByProjectRequirementAsync para recibir userId y projectRequirementId.
* Se actualizó ProposalsController para enviar el userId autenticado al servicio.

## Razón del ajuste

El endpoint de propuestas por requerimiento podía exponer información sensible de competencia entre contratistas, como precio, tiempos estimados, garantías y descripción de la propuesta.

El comportamiento correcto para el modelo de OfiPro es que solo el cliente dueño del proyecto pueda ver todas las propuestas recibidas.

## Resultado

El endpoint GET /api/proposals/requirement/{projectRequirementId} quedó protegido.

Comportamiento validado:

* Dueño del proyecto → 200 OK.
* Usuario no dueño → 403 Forbidden.
* Requerimiento inexistente → 404 Not Found.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

## Pendiente inmediato

Antes de iniciar Bloque 7 - Evidencias, revisar duda conceptual surgida del Bloque 6.

## Próximo paso

Conversar y resolver la duda puntual relacionada con Bloque 6 antes de continuar con Evidencias.


# =====================================

# SESIÓN 2026-06-20

## Objetivo

Iniciar y completar Bloque 7 - Evidencias V1.

# =====================================

## Decisión estratégica previa

Antes de iniciar Evidencias se discutió la orientación futura de plataforma.

Conclusión:

OfiPro será pensado como plataforma mobile-first, pero no se tomará el camino PWA.

Ruta acordada:

* Terminar backend/API correctamente.
* Crear web responsiva necesaria.
* En una etapa posterior, desarrollar app móvil real.
* Evitar PWA como paso intermedio para no consumir recursos en una ruta que no se considera prioritaria.

Razón:

El usuario final probablemente usará OfiPro desde celular, especialmente para evidencias, seguimiento, ubicación, notificaciones y operación en campo. Sin embargo, abrir el frente móvil ahora sería prematuro. Primero se debe consolidar el backend.

# =====================================

## Bloque 7 - Evidencias V1

Completado:

* Se creó la entidad Evidence en Domain.
* Se agregó DbSet<Evidence> en ApplicationDbContext.
* Se creó EvidenceConfiguration.
* Se generó migración AddEvidences.
* Se actualizó la base de datos.
* Se creó EvidenceDto.
* Se creó CreateEvidenceDto.
* Se creó IEvidenceRepository.
* Se implementó EvidenceRepository.
* Se creó IEvidenceService.
* Se implementó EvidenceService.
* Se registraron IEvidenceRepository y IEvidenceService en Program.cs.
* Se creó EvidencesController.

Endpoints creados:

* POST /api/contracts/{contractId}/evidences
* GET /api/contracts/{contractId}/evidences
* DELETE /api/evidences/{evidenceId}

Reglas implementadas:

* Solo el contratista de la contratación puede subir evidencias.
* Cliente y contratista pueden consultar evidencias del contrato.
* Usuarios ajenos no pueden consultar evidencias.
* Cliente no puede subir evidencias.
* Las evidencias no se suben a contratos finalizados o cancelados.
* Las evidencias no se eliminan si el contrato está finalizado o cancelado.
* Solo el usuario que subió la evidencia puede eliminarla.
* La eliminación se realiza mediante soft delete.

Pruebas realizadas:

* Se creó un contrato limpio desde flujo real:

  * Cliente crea proyecto.
  * Contratista envía propuesta.
  * Cliente acepta propuesta.
  * Sistema crea contrato automáticamente.
* Contratista subió evidencia correctamente.
* Evidencia apareció en tabla Evidences.
* Contratista consultó evidencias correctamente.
* Cliente dueño consultó evidencias correctamente.
* Cliente intentó subir evidencia y recibió 403.
* Contratista eliminó evidencia correctamente.
* DeletedAt se asignó en BD.
* Evidencia eliminada ya no apareció en consultas.

Resultado:

Bloque 7 - Evidencias V1 quedó completado funcionalmente.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

## Pendiente

Definir siguiente bloque.

Opciones posibles:

* Calificaciones y reputación.
* Perfil profesional del contratista.
* Carga real de archivos para evidencias.
* Mejoras de flujo de contratación.

## Observación

El flujo principal de OfiPro ya conecta:

Proyecto → Requerimiento → Propuesta → Contrato → Evidencia

Esto representa un avance importante hacia un MVP funcional real.



# =====================================

# SESIÓN 2026-06-20

## Objetivo

Resolver diagnóstico posterior al Bloque 7 - Evidencias V1.

# =====================================

## Bloque 7.1 - Corrección de diagnóstico de Evidencias

Diagnóstico recibido:

* EvidenceType existía en dominio pero no se usaba.
* FileType era un string libre sin restricción de formato.
* EvidencesController fue verificado y el binding estaba correcto gracias a ApiController.

Decisión tomada:

No eliminar EvidenceType. Se decidió integrarlo formalmente al módulo de Evidencias porque aporta valor al negocio.

Valores definidos:

* Antes = 1
* Durante = 2
* Despues = 3

Completado:

* Se agregó EvidenceType a la entidad Evidence.
* Se agregó EvidenceType a EvidenceDto.
* Se agregó EvidenceType a CreateEvidenceDto.
* Se agregó validación de EvidenceType en CreateEvidenceDto.
* Se configuró EvidenceType en EvidenceConfiguration.
* Se generó migración AddEvidenceTypeToEvidences.
* Se aplicó migración a la base de datos.
* Se agregó validación de FileType permitido.
* FileType quedó limitado a image/jpeg, image/png y application/pdf.
* Se normalizó FileType antes de guardar.

Pruebas realizadas:

* Crear evidencia con EvidenceType válido y FileType image/jpeg → 200 OK.
* Crear evidencia con FileType inválido → 400 Bad Request.
* Crear evidencia con EvidenceType inválido → 400 Bad Request.
* Consultar evidencia creada → devuelve EvidenceType correctamente.
* Base de datos guarda EvidenceType correctamente.

Resultado:

El módulo de Evidencias queda más ordenado, útil y seguro.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

## Pendiente

Definir el siguiente bloque funcional.

Opciones posibles:

* Calificaciones y reputación.
* Perfil profesional del contratista.
* Carga real de archivos para evidencias.
* Mejoras de flujo de contratación.

## Observación

EvidenceType refuerza el enfoque mobile-first futuro, porque permitirá organizar evidencias tomadas desde celular según la etapa del trabajo: antes, durante y después.


# =====================================

# SESIÓN 2026-06-20

## Objetivo

Revisar implicaciones de cultura informática, comodidad humana y uso real de OfiPro antes de continuar con nuevos módulos.

# =====================================

## Discusión estratégica

Se analizó que, aunque la web responsiva es necesaria, el usuario operativo de OfiPro probablemente usará más el celular que una computadora.

Se identificó un riesgo importante:

Un contratista podría no tener el hábito de abrir Google, buscar la página, iniciar sesión y revisar manualmente si tiene nuevas propuestas, contratos o evidencias pendientes.

Conclusión:

La app móvil real no debe considerarse un lujo futuro, sino una pieza importante para la operación diaria del contratista.

Sin embargo, no se abrirá el frente móvil todavía. Primero se continuará consolidando el backend/API.

## Decisiones tomadas

* La web seguirá siendo necesaria.
* La PWA sigue descartada.
* La app móvil real se mantiene como etapa futura importante.
* La API debe seguir preparándose para web y móvil.
* El backend debe empezar a considerar comportamiento mobile-first.
* El próximo bloque será Notificaciones internas base.

## Razón del ajuste

En un marketplace, no basta con que existan funciones. También debe existir actividad.

Si el contratista no se entera rápido de nuevas oportunidades o cambios en sus contratos, no participa.

Si el contratista no participa, el cliente no recibe propuestas.

Si el cliente no recibe propuestas, la plataforma pierde valor.

## Próximo bloque definido

Bloque 7.2 - Notificaciones internas base.

Objetivo del próximo bloque:

Crear el módulo base de notificaciones internas para registrar eventos importantes dentro del sistema, aunque todavía no existan push notifications reales.

Posibles endpoints:

* GET /api/notifications
* GET /api/notifications/unread-count
* PATCH /api/notifications/{notificationId}/read
* PATCH /api/notifications/read-all

Eventos futuros a notificar:

* Nueva propuesta recibida.
* Propuesta aceptada.
* Propuesta rechazada.
* Contrato creado.
* Contrato iniciado.
* Evidencia subida.
* Contrato finalizado.

## Estado al cierre

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Pendiente por iniciar

## Nota

Se recomienda continuar en un nuevo chat debido a la lentitud acumulada de la conversación actual.


# =====================================

# SESIÓN 2026-06-21

## Objetivo

Iniciar, implementar, probar y cerrar Bloque 7.2 - Notificaciones internas base.

# =====================================

## Contexto

Antes de iniciar el bloque se confirmó que OfiPro será una plataforma mobile-first, pero sin construir una PWA.

La estrategia sigue siendo:

* Terminar backend/API correctamente.
* Mantener web responsiva como canal necesario.
* Dejar app móvil real para una etapa posterior.
* Preparar el backend para comportamiento móvil desde ahora.

El motivo principal del bloque fue evitar que la plataforma dependa de que el usuario revise manualmente la web para enterarse de nuevas propuestas, cambios de estado o evidencias.

## Bloque 7.2 - Notificaciones internas base

Completado:

* Se creó NotificationType.
* Se creó la entidad Notification.
* Se creó NotificationConfiguration.
* Se agregó DbSet<Notification> en ApplicationDbContext.
* Se generó migración para Notifications.
* Se actualizó la base de datos.
* Se creó NotificationDto.
* Se creó CreateNotificationDto.
* Se creó INotificationRepository.
* Se implementó NotificationRepository.
* Se creó INotificationService.
* Se implementó NotificationService.
* Se registraron INotificationRepository e INotificationService en Program.cs.
* Se creó NotificationsController.

Endpoints creados:

* GET /api/notifications
* GET /api/notifications/unread
* GET /api/notifications/unread-count
* PATCH /api/notifications/{notificationId}/read
* PATCH /api/notifications/read-all
* DELETE /api/notifications/{notificationId}

## Reglas implementadas

* Las notificaciones pertenecen a un usuario específico.
* El UserId se obtiene desde el JWT.
* Un usuario solo puede consultar sus propias notificaciones.
* Un usuario solo puede marcar como leídas sus propias notificaciones.
* Un usuario solo puede eliminar sus propias notificaciones.
* Las notificaciones eliminadas usan soft delete mediante DeletedAt.
* No existe endpoint público para crear notificaciones.
* Las notificaciones se crean desde servicios de negocio.
* Las fechas se guardan en UTC.

## Eventos conectados

Propuestas:

* Contratista envía propuesta → cliente recibe notificación.
* Cliente acepta propuesta → contratista recibe notificación.
* Cliente rechaza propuesta → contratista recibe notificación.
* Cuando una propuesta es aceptada y otras pendientes se rechazan automáticamente, los contratistas rechazados reciben notificación.

Evidencias:

* Contratista sube evidencia → cliente recibe notificación.

Contrataciones:

* Contratista cambia contrato a EnProceso → cliente recibe notificación.
* Contratista cambia contrato a PendienteConfirmacion → cliente recibe notificación.
* Cliente cambia contrato a Finalizado → contratista recibe notificación.
* Cliente o contratista cancela contrato → la otra parte recibe notificación.

## Pruebas realizadas

Notificaciones base:

* Notificación insertada manualmente en SQL Server.
* GET /api/notifications → 200 OK.
* GET /api/notifications/unread → 200 OK.
* GET /api/notifications/unread-count → 200 OK.
* PATCH /api/notifications/{notificationId}/read → 200 OK.
* PATCH /api/notifications/read-all → 200 OK.
* DELETE /api/notifications/{notificationId} → 204 No Content.
* Soft delete validado en base de datos.

Flujo de propuestas:

* Cliente crea proyecto nuevo.
* Contratista crea propuesta.
* Cliente recibe notificación por nueva propuesta.
* Cliente acepta propuesta.
* Contratista recibe notificación por propuesta aceptada.
* Cliente rechaza propuesta.
* Contratista recibe notificación por propuesta rechazada.

Flujo de evidencias:

* Contratista sube evidencia.
* Cliente recibe notificación por nueva evidencia.
* Contador de no leídas validado desde Swagger.

Flujo de contrataciones:

* Contratista cambia contratación a EnProceso.
* Cliente recibe notificación.
* Contratista cambia contratación a PendienteConfirmacion.
* Cliente recibe notificación.
* Cliente cambia contratación a Finalizado.
* Contratista recibe notificación.

## Observación técnica

Durante las pruebas se detectó que SQL Server muestra horas en UTC, por ejemplo una hora aparentemente distinta a la hora local de México.

Decisión:

No cambiar a DateTime.Now.

Se mantiene DateTime.UtcNow en backend y base de datos.

Razón:

El backend debe guardar fechas de forma consistente para web, app móvil y posibles usuarios en diferentes zonas horarias. La conversión a hora local debe hacerse al mostrar la información en frontend o app móvil.

## Resultado

Bloque 7.2 - Notificaciones internas base quedó completado y probado.

El backend ya registra eventos reales del marketplace como notificaciones internas.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Completo

## Evaluación de velocidad

Ritmo: 🟡 Medio.

El bloque se sintió pesado porque cruzó varias capas y varios módulos ya existentes:

* Domain.
* Application.
* Infrastructure.
* Api.
* Base de datos.
* Propuestas.
* Evidencias.
* Contrataciones.
* Pruebas con Swagger.
* Validaciones con SQL Server.

Aunque tomó más tiempo, el avance fue correcto porque cada flujo quedó probado antes de cerrar el bloque.

## Pendiente inmediato

* Ejecutar build final.
* Revisar git status.
* Hacer commit del Bloque 7 completo.
* Subir cambios al repositorio.

## Próximo bloque recomendado

Bloque 8 - Calificaciones y reputación V1.

Razón:

Después de que una contratación finaliza, OfiPro necesita cerrar el ciclo con una calificación para construir confianza, historial y reputación del contratista.

# =====================================


# SESIÓN 2026-06-21

## Objetivo

Iniciar, implementar, probar y cerrar Bloque 8 - Calificaciones y reputación V1, además de reforzar el módulo con Bloque 8.1 - Endurecimiento de Ratings y reputación.

# =====================================

## Contexto

Después de cerrar Notificaciones internas base, se decidió continuar con Calificaciones y reputación porque el flujo de OfiPro necesitaba cerrar el ciclo de confianza después de una contratación finalizada.

Decisión previa importante:

Las calificaciones no serán solamente del cliente hacia el contratista.

El modelo correcto para OfiPro será bidireccional:

* Cliente califica al contratista.
* Contratista califica al cliente.

Razón:

OfiPro no solo necesita contratistas confiables. También necesita clientes confiables que expliquen bien el trabajo, respeten acuerdos y mantengan buena comunicación.

## Bloque 8 - Calificaciones y reputación V1

Completado:

* Se revisó el estado actual de Rating.
* Se detectó que Rating ya existía, pero estaba orientado a Project.
* Se corrigió Rating para asociarse a Contract.
* Se reemplazaron campos anteriores por:
  * ContractId
  * RaterUserId
  * RatedUserId
  * Score
  * Comment
  * CreatedAt
  * DeletedAt
* Se actualizó RatingConfiguration.
* Se configuró relación Rating → Contract.
* Se configuraron relaciones Rating → RaterUser y Rating → RatedUser.
* Se agregó restricción única por ContractId, RaterUserId y RatedUserId.
* Se agregó check constraint para Score entre 1 y 5.
* Se creó CreateRatingDto.
* Se creó RatingDto.
* Se creó UserReputationDto.
* Se creó IRatingRepository.
* Se implementó RatingRepository.
* Se creó IRatingService.
* Se implementó RatingService.
* Se registraron IRatingRepository e IRatingService en Program.cs.
* Se creó RatingsController.
* Se generó migración UpdateRatingsForContracts.
* Se actualizó la base de datos.

Endpoints creados:

* POST /api/contracts/{contractId}/ratings
* GET /api/contracts/{contractId}/ratings
* GET /api/users/{userId}/reputation

Reglas implementadas:

* Solo se puede calificar si el contrato está Finalizado.
* Solo cliente y contratista del contrato pueden calificar.
* Un usuario no puede calificarse a sí mismo.
* Solo debe existir una calificación por dirección por contrato.
* Cliente puede calificar al contratista.
* Contratista puede calificar al cliente.
* El usuario que califica se obtiene desde el JWT.
* El usuario calificado se calcula automáticamente desde el contrato.
* Score debe estar entre 1 y 5.

Pruebas realizadas:

* Se probó contrato finalizado.
* Cliente calificó al contratista → 200 OK.
* Contratista calificó al cliente → 200 OK.
* GET /api/contracts/{contractId}/ratings → 200 OK.
* Se validó que aparecieran las dos direcciones:
  * Cliente → Contratista
  * Contratista → Cliente
* Se verificaron registros en SQL Server.
* GET /api/users/{userId}/reputation → 200 OK.

Resultado:

Bloque 8 - Calificaciones y reputación V1 quedó completado y probado correctamente.

## Bloque 8.1 - Endurecimiento de Ratings y reputación

Objetivo:

Mejorar el módulo recién creado para dejarlo más útil para perfiles públicos, web responsiva y futura app móvil real.

Completado:

* Se agregó LastRatingAt a UserReputationDto.
* Se mejoró GetByRatedUserIdAsync para incluir RaterUser y RatedUser.
* Se creó endpoint para historial interno de calificaciones recibidas.
* Se creó PublicReceivedRatingDto.
* Se creó PublicUserReputationDto.
* Se creó endpoint público limpio de ratings recibidos.
* Se creó endpoint público completo de reputación.
* Se separó la información interna de la información pública.

Endpoints creados:

* GET /api/users/{userId}/ratings/received
* GET /api/users/{userId}/ratings/public
* GET /api/users/{userId}/reputation/public

Reglas aplicadas:

* El historial interno puede mostrar datos completos.
* La vista pública no expone ContractId.
* La vista pública no expone RaterUserId.
* La vista pública no expone RatedUserId.
* La reputación pública devuelve promedio, total, última fecha y comentarios recibidos.
* La app móvil podrá obtener la reputación pública completa en una sola llamada.

Pruebas realizadas:

* GET /api/users/{userId}/reputation con LastRatingAt → 200 OK.
* GET /api/users/{userId}/ratings/received → 200 OK.
* GET /api/users/{userId}/ratings/public → 200 OK.
* GET /api/users/{userId}/reputation/public → 200 OK.
* Se validó que las respuestas públicas no expusieran IDs internos innecesarios.

Resultado:

Bloque 8.1 - Endurecimiento de Ratings y reputación quedó completado y probado correctamente.

## Decisiones importantes

D043

Las calificaciones deben ser bidireccionales.

D044

Una contratación solo puede calificarse cuando está finalizada.

D045

Solo cliente y contratista de una contratación pueden calificarla.

D046

Solo puede existir una calificación por dirección en cada contratación.

D047

La reputación pública no debe exponer datos internos de la contratación.

D048

La app móvil debe poder consultar reputación pública en una sola llamada.

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Completo

Bloque 8 - Calificaciones y reputación V1 → Completo

Bloque 8.1 - Endurecimiento de Ratings y reputación → Completo

## Evaluación de velocidad

Ritmo: 🟢 Bueno.

El avance fue más rápido que el bloque de notificaciones porque el módulo de Ratings fue más lineal y se apoyó en patrones ya existentes:

* DTOs.
* Repositories.
* Services.
* Controller.
* Migración.
* Swagger.
* Validación con SQL Server.

El bloque 8.1 también avanzó bien porque no requirió migración y se enfocó en mejorar endpoints de lectura y separación entre datos internos y públicos.

## Pendiente inmediato

* Actualizar documentación.
* Revisar git status.
* Hacer commit del Bloque 8 y Bloque 8.1.
* Subir cambios al repositorio.

## Próximo bloque recomendado

Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web.

Razón:

El backend ya cuenta con módulos principales de flujo:

* Auth.
* Usuarios.
* Proyectos.
* Propuestas.
* Contrataciones.
* Evidencias.
* Notificaciones internas.
* Calificaciones.
* Reputación.

El siguiente paso lógico es crear endpoints de resumen para que web y futura app móvil no tengan que hacer muchas llamadas separadas para construir sus pantallas principales.

# =====================================


# =====================================

# SESIÓN 2026-06-22

## Objetivo

Continuar OfiPro, implementar Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web, y corregir pendientes menores detectados en el diagnóstico de Bloque 8.

# =====================================

## Contexto inicial

El proyecto ya contaba con:

* Auth.
* Usuarios.
* Proyectos.
* Propuestas.
* Contrataciones.
* Evidencias.
* Notificaciones internas.
* Ratings y reputación.

Antes de avanzar a nuevos módulos grandes, se decidió crear endpoints de resumen para web responsiva y futura app móvil, evitando que las pantallas principales necesiten demasiadas llamadas separadas.

También se recibió un diagnóstico del Bloque 8 donde se detectaron dos puntos menores:

* Faltaba notificación cuando un usuario recibía una calificación.
* Existía duplicación entre métodos de reputación.

# =====================================

## Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web

Completado:

* Se creó carpeta DTOs/Dashboard.
* Se creó ClientDashboardSummaryDto.
* Se creó DashboardNotificationDto.
* Se creó ClientDashboardContractDto.
* Se creó ContractorDashboardSummaryDto.
* Se creó ContractorDashboardContractDto.
* Se creó ContractorDashboardProposalDto.
* Se creó ContractorAvailableProjectDto.
* Se creó ClientPendingProposalDto.
* Se creó AdminDashboardSummaryDto.
* Se creó DashboardModesDto.
* Se creó DashboardUserContextDto.
* Se creó IDashboardRepository.
* Se implementó DashboardRepository.
* Se creó IDashboardService.
* Se implementó DashboardService.
* Se creó DashboardController.
* Se registraron IDashboardRepository e IDashboardService en Program.cs.

Endpoints creados:

* GET /api/dashboard/client/summary
* GET /api/dashboard/contractor/summary
* GET /api/dashboard/admin/summary
* GET /api/dashboard/modes
* GET /api/dashboard/me

## Dashboard de cliente

Incluye:

* Total de proyectos.
* Proyectos abiertos.
* Propuestas pendientes recibidas.
* Contratos activos.
* Contratos pendientes de confirmación.
* Contratos finalizados.
* Notificaciones no leídas.
* Notificaciones recientes.
* Contratos recientes.
* Vista previa de propuestas pendientes.

## Dashboard de contratista

Incluye:

* Proyectos disponibles.
* Propuestas enviadas.
* Propuestas pendientes.
* Propuestas aceptadas.
* Propuestas rechazadas.
* Contratos activos.
* Contratos pendientes de inicio.
* Contratos en proceso.
* Contratos pendientes de confirmación.
* Contratos finalizados.
* Notificaciones no leídas.
* Promedio de calificación.
* Total de calificaciones.
* Notificaciones recientes.
* Contratos recientes.
* Propuestas recientes.
* Vista previa de proyectos disponibles.

## Dashboard de administrador

Incluye:

* Total de usuarios.
* Total de clientes.
* Total de contratistas.
* Total de administradores.
* Total de proyectos.
* Proyectos publicados.
* Proyectos asignados.
* Proyectos finalizados.
* Total de contratos.
* Contratos activos.
* Contratos finalizados.
* Contratos cancelados.
* Total de calificaciones.
* Notificaciones no leídas.

## Modos disponibles

Se creó endpoint:

GET /api/dashboard/modes

Devuelve:

* UserId.
* CanUseClientMode.
* CanUseContractorMode.
* CanUseAdminMode.
* AvailableModes.
* DefaultMode.

Razón:

El frontend o app móvil debe saber qué modos mostrar sin adivinar a partir del token o hacer lógica duplicada.

## Contexto del usuario autenticado

Se creó endpoint:

GET /api/dashboard/me

Devuelve:

* UserId.
* Name.
* LastName.
* FullName.
* Email.
* Modes.

Razón:

Después del login, la aplicación cliente necesita identificar rápidamente al usuario y sus modos disponibles.

# =====================================

## Ajuste de roles para pruebas

Durante las pruebas se detectó que [cliente@ofipro.com](mailto:cliente@ofipro.com) tenía también rol Contratista, lo que impedía probar correctamente respuestas 403 en el dashboard de contratista.

Decisión tomada:

Separar usuarios de prueba por escenario.

Resultado:

* [cliente@ofipro.com](mailto:cliente@ofipro.com) queda como Cliente puro.
* [contratista@ofipro.com](mailto:contratista@ofipro.com) queda como Contratista puro.
* [admin@ofipro.com](mailto:admin@ofipro.com) queda como usuario multirol para pruebas.

Razón:

Un usuario multirol puede consumir correctamente más de un dashboard, pero no sirve para probar restricciones negativas de rol.

# =====================================

## Pruebas de Bloque 9

Pruebas realizadas con [cliente@ofipro.com](mailto:cliente@ofipro.com):

* GET /api/dashboard/client/summary → 200 OK.
* GET /api/dashboard/contractor/summary → 403 Forbidden.
* GET /api/dashboard/admin/summary → 403 Forbidden.
* GET /api/dashboard/modes → 200 OK.
* GET /api/dashboard/me → 200 OK.

Pruebas realizadas con [contratista@ofipro.com](mailto:contratista@ofipro.com):

* GET /api/dashboard/contractor/summary → 200 OK.
* GET /api/dashboard/client/summary → 403 Forbidden.
* GET /api/dashboard/admin/summary → 403 Forbidden.
* GET /api/dashboard/modes → 200 OK.
* GET /api/dashboard/me → 200 OK.

Pruebas realizadas con [admin@ofipro.com](mailto:admin@ofipro.com):

* GET /api/dashboard/client/summary → 200 OK.
* GET /api/dashboard/contractor/summary → 200 OK.
* GET /api/dashboard/admin/summary → 200 OK.
* GET /api/dashboard/modes → 200 OK.
* GET /api/dashboard/me → 200 OK.

Resultado:

Bloque 9 quedó completado y probado correctamente.

# =====================================

## Bloque 8.2 - Correcciones de diagnóstico de Ratings y reputación

Diagnóstico recibido:

* Faltaba notificación al recibir una calificación.
* GetUserReputationAsync y GetPublicUserReputationAsync duplicaban lógica de reputación.

## RatingReceived notification

Completado:

* Se agregó RatingReceived a NotificationType.
* Se inyectó INotificationService en RatingService.
* Se generó notificación interna al usuario calificado dentro de RatingService.CreateAsync.
* La notificación usa RelatedEntityType = Rating.
* La notificación usa RelatedEntityId con el Id de la calificación creada.

Pruebas realizadas:

* Cliente calificó contratista.
* Contratista recibió notificación RatingReceived.
* Contratista calificó cliente.
* Cliente recibió notificación RatingReceived.
* GET /api/notifications devolvió la notificación correctamente.
* Se validó Type = 11.
* Se validó IsRead = false.
* Se validó ReadAt = null.

Resultado:

El evento de calificación recibida ya queda cubierto por el sistema de notificaciones internas.

## Refactor de reputación

Completado:

* Se eliminó duplicación de lógica entre métodos de reputación.
* Se agregó método privado para obtener usuario activo.
* Se agregó método privado para calcular estadísticas de reputación.
* Se agregó método privado para mapear ratings públicos.
* GetUserReputationAsync reutiliza la lógica común.
* GetPublicUserReputationAsync reutiliza la lógica común.
* GetPublicReceivedByUserIdAsync reutiliza mapeo público.

Pruebas realizadas:

* GET /api/users/{userId}/reputation → 200 OK.
* GET /api/users/{userId}/ratings/public → 200 OK.
* GET /api/users/{userId}/reputation/public → 200 OK.

Resultado:

El módulo de Ratings y reputación queda más limpio y mantenible.

# =====================================

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Completo

Bloque 8 - Calificaciones y reputación V1 → Completo

Bloque 8.1 - Endurecimiento de Ratings y reputación → Completo

Bloque 8.2 - Correcciones de diagnóstico de Ratings y reputación → Completo

Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web → Completo

# =====================================

## Evaluación de velocidad

Ritmo: 🟢 Bueno.

El avance fue sólido porque se construyó sobre patrones ya existentes:

* DTOs.
* Repositories.
* Services.
* Controllers.
* Validación de roles.
* Notificaciones internas.
* Pruebas con Swagger.
* Verificación con SQL Server.

El bloque no requirió migraciones y eso permitió avanzar más rápido.

# =====================================

## Pendiente inmediato

* Revisar git status.
* Hacer commit del Bloque 8.2 y Bloque 9.
* Subir cambios al repositorio.

# =====================================

## Próximo bloque recomendado

Bloque 10 - ProfessionalProfile.

Razón:

El diagnóstico indica que la entidad y configuración EF ya existen, pero falta completar:

* DTOs.
* Repository.
* Service.
* Controller.
* Endpoints.
* Pruebas.

Este módulo es crítico porque permitirá construir el perfil profesional del contratista y preparar la futura búsqueda/discovery de proveedores.

# =====================================

# =====================================

# SESIÓN 2026-06-22 (PARTE 2)

## Objetivo

Resolver diagnóstico menor del Bloque 9 y ajustar la estrategia de lanzamiento de OfiPro.

# =====================================

## Diagnóstico recibido

Se identificaron dos observaciones menores:

* DashboardRepository accede directamente a ApplicationDbContext.
* El conteo de usuarios por rol en el dashboard administrativo no filtraba usuarios eliminados.

## Corrección aplicada

Se actualizó DashboardRepository.GetAdminSummaryAsync para que los conteos por rol respeten soft delete.

Ajustes realizados:

* TotalClients ahora filtra User.DeletedAt == null.
* TotalContractors ahora filtra User.DeletedAt == null.
* TotalAdmins ahora filtra User.DeletedAt == null.

Resultado:

El dashboard administrativo ya no cuenta usuarios eliminados lógicamente dentro de las métricas por rol.

## Decisión documentada

Se decidió documentar que DashboardRepository puede consultar directamente ApplicationDbContext para lecturas agregadas.

Razón:

El dashboard necesita combinar información de varios módulos:

* Projects
* Proposals
* Contracts
* Notifications
* Ratings
* UserRoles

Esto se considera válido únicamente para endpoints de lectura agregada o read-models de dashboard.

No debe usarse para saltarse reglas de negocio ni para operaciones de escritura.

## Ajuste estratégico de lanzamiento

Se aclaró que OfiPro no debe lanzarse al mercado solo con web.

Estrategia actual:

* Terminar backend/API.
* Construir web responsiva suficiente.
* Construir app móvil real.
* Lanzar cuando web y app móvil estén listas para un flujo usable.

Razón:

OfiPro depende mucho de usuarios en campo. La app móvil es crítica para notificaciones, evidencias, seguimiento y respuesta rápida de contratistas.

Impacto:

Los siguientes módulos pasan a considerarse preparación pre-lanzamiento móvil:

* Refresh tokens.
* Upload real de archivos.
* FCM Token.
* Push notifications.

## Pruebas realizadas

* dotnet build → correcto.
* GET /api/dashboard/admin/summary con admin → 200 OK.
* Métricas administrativas por rol validadas correctamente.
* Reglas de autorización de dashboards se mantienen correctas.

## Resultado

Diagnóstico menor de Bloque 9 corregido y documentado.

# =====================================

# =====================================

# SESIÓN 2026-06-23

## Objetivo

Implementar Bloque 10 - ProfessionalProfile y búsqueda básica de contratistas.

# =====================================

## Contexto

Después de completar dashboard, ratings, reputación y notificaciones internas, se decidió avanzar con ProfessionalProfile porque era el último módulo crítico pendiente para discovery de contratistas.

El diagnóstico previo señalaba que la entidad ProfessionalProfile y su configuración EF ya existían, pero faltaba implementar:

* DTOs.
* Repository.
* Service.
* Controller.
* Endpoints.
* Pruebas.

# =====================================

## Bloque 10 - ProfessionalProfile y búsqueda básica de contratistas

Completado:

* Se creó carpeta DTOs/ProfessionalProfile.
* Se creó CreateProfessionalProfileDto.
* Se creó UpdateProfessionalProfileDto.
* Se creó ProfessionalProfileDto.
* Se creó ContractorSearchDto.
* Se creó IProfessionalProfileRepository.
* Se implementó ProfessionalProfileRepository.
* Se creó IProfessionalProfileService.
* Se implementó ProfessionalProfileService.
* Se registraron IProfessionalProfileRepository e IProfessionalProfileService en Program.cs.
* Se creó ProfessionalProfilesController.
* Se creó endpoint para crear perfil profesional.
* Se creó endpoint para consultar perfil profesional propio.
* Se creó endpoint para actualizar perfil profesional propio.
* Se creó endpoint para búsqueda básica de contratistas.
* Se creó endpoint para consultar perfil público de contratista por UserId.
* Se integró el estado del perfil profesional al dashboard del contratista.

Endpoints creados:

* POST /api/professional-profiles
* GET /api/professional-profiles/me
* PUT /api/professional-profiles/me
* GET /api/contractors
* GET /api/contractors/{userId}

Endpoint actualizado:

* GET /api/dashboard/contractor/summary

# =====================================

## Reglas implementadas

* Solo usuarios con rol Contratista pueden crear perfil profesional.
* Solo usuarios con rol Contratista pueden consultar su perfil profesional.
* Solo usuarios con rol Contratista pueden actualizar su perfil profesional.
* Un usuario no puede crear más de un perfil profesional.
* Los perfiles profesionales pueden activarse o desactivarse.
* Los perfiles inactivos no aparecen en búsqueda pública.
* Los usuarios eliminados lógicamente no aparecen en búsqueda pública.
* Los usuarios inactivos no aparecen en búsqueda pública.
* La búsqueda pública solo devuelve usuarios con rol Contratista.
* La búsqueda permite filtros por specialty, state y city.
* El dashboard del contratista muestra si ya existe perfil profesional.

# =====================================

## Pruebas realizadas

Con [cliente@ofipro.com](mailto:cliente@ofipro.com):

* POST /api/professional-profiles → 403 Forbidden.
* GET /api/contractors → 200 OK.

Con [contratista@ofipro.com](mailto:contratista@ofipro.com):

* POST /api/professional-profiles → 200 OK.
* GET /api/professional-profiles/me → 200 OK.
* POST /api/professional-profiles duplicado → 400 Bad Request.
* PUT /api/professional-profiles/me → 200 OK.
* GET /api/contractors → 200 OK.
* GET /api/contractors?specialty=plomería → 200 OK.
* GET /api/contractors?city={ciudad} → 200 OK.
* GET /api/contractors/{userId} → 200 OK.
* GET /api/dashboard/contractor/summary → 200 OK.

Prueba de perfil inactivo:

* Se actualizó perfil profesional con IsActive = false.
* GET /api/contractors dejó de mostrar al contratista.
* Se actualizó perfil profesional con IsActive = true.
* GET /api/contractors volvió a mostrar al contratista.

Resultado:

Bloque 10 quedó completado y probado correctamente.

# =====================================

## Decisiones importantes

D056

ProfessionalProfile será la base del descubrimiento de contratistas.

D057

La búsqueda básica de contratistas entra en V1.

D058

El dashboard del contratista debe mostrar el estado de su perfil profesional.

# =====================================

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Completo

Bloque 8 - Calificaciones y reputación V1 → Completo

Bloque 8.1 - Endurecimiento de Ratings y reputación → Completo

Bloque 8.2 - Correcciones de diagnóstico de Ratings y reputación → Completo

Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web → Completo

Bloque 10 - ProfessionalProfile y búsqueda básica de contratistas → Completo

# =====================================

## Evaluación de velocidad

Ritmo: 🟢 Bueno.

El bloque avanzó rápido porque la entidad ProfessionalProfile y la configuración EF ya existían. El trabajo principal fue completar el stack de aplicación:

* DTOs.
* Repository.
* Service.
* Controller.
* Endpoints.
* Pruebas.
* Integración con dashboard.

No se requirió migración.

# =====================================

## Pendiente inmediato

* Actualizar documentación.
* Revisar git status.
* Hacer commit del Bloque 10.
* Subir cambios al repositorio.

# =====================================

## Próximo bloque recomendado

Bloque 11 - Expiración automática de proyectos.

Razón:

ProjectStatus ya tiene el valor Expirado, pero todavía no existe un proceso automático que marque proyectos publicados antiguos como expirados. Esto es importante para evitar proyectos fantasma en el feed del contratista.

# =====================================


# =====================================

## Corrección de diagnóstico Bloque 10

Después de completar ProfessionalProfile y búsqueda básica de contratistas, se revisó diagnóstico técnico del bloque.

Hallazgos:

* SearchContractorsAsync tenía riesgo de N+1 queries al consultar ratings dentro de un foreach.
* No existía una migración separada AddProfessionalProfiles, aunque la tabla ProfessionalProfiles ya existía desde InitialCreate.

# =====================================

## Corrección aplicada: N+1 queries

Se agregó en IRatingRepository:

GetReputationStatsByUserIdsAsync(List<Guid> userIds)

Se implementó en RatingRepository usando:

* Filtro por RatedUserId.
* Filtro DeletedAt == null.
* GroupBy por RatedUserId.
* Average para Score.
* Count para TotalRatings.

Se actualizó SearchContractorsAsync en ProfessionalProfileService para:

* Obtener primero los perfiles profesionales.
* Obtener la lista de UserIds.
* Consultar estadísticas de reputación en una sola operación.
* Construir los ContractorSearchDto sin consultar base de datos dentro del foreach.

Resultado:

La búsqueda de contratistas pasó de una posible consulta por contratista a una consulta agrupada para reputación.

# =====================================

## Verificación de migraciones ProfessionalProfiles

Se verificó si EF Core detectaba cambios pendientes en el modelo.

Resultado:

EF Core no detectó cambios pendientes.

Conclusión:

No se requiere migración AddProfessionalProfiles porque ProfessionalProfiles ya formaba parte del esquema inicial desde InitialCreate y el snapshot está sincronizado.

# =====================================

## Pruebas posteriores

Se probaron nuevamente:

* GET /api/contractors.
* GET /api/contractors?specialty=plomería.
* GET /api/contractors/{userId}.

Resultado:

Todas las pruebas fueron correctas.

# =====================================

## Estado del diagnóstico

P025 - N+1 queries en SearchContractorsAsync → Corregido.

P026 - Verificación de snapshot ProfessionalProfiles → Correcto, sin migración pendiente.

# =====================================

# =====================================

# SESIÓN 2026-06-24

## Objetivo

Implementar, probar y cerrar Bloque 11 - Expiración automática de proyectos.

# =====================================

## Contexto

Después de completar ProfessionalProfile, búsqueda básica de contratistas y la corrección de diagnóstico del Bloque 10, el siguiente problema detectado fue que ProjectStatus ya tenía el valor Expirado, pero no existía un proceso automático que marcara proyectos antiguos como expirados.

Esto podía provocar que el feed del contratista mostrara proyectos viejos o fantasma.

# =====================================

## Bloque 11 - Expiración automática de proyectos

Completado:

* Se confirmó que ProjectStatus.Expirado ya existía.
* Se confirmó que Project.CreatedAt ya existía.
* Se confirmó que no era necesaria una migración.
* Se agregó ExpirePublishedProjectsAsync en IProjectRepository.
* Se implementó ExpirePublishedProjectsAsync en ProjectRepository.
* Se usó actualización masiva para expirar proyectos publicados antiguos.
* Se creó ProjectExpirationBackgroundService.
* Se registró ProjectExpirationBackgroundService como HostedService en Program.cs.
* Se agregó configuración ProjectExpiration en appsettings.json.
* Se ajustó GET /api/projects para devolver solo proyectos publicados activos.

Configuración agregada:

* ProjectExpiration:ExpirationDays
* ProjectExpiration:CheckIntervalHours

# =====================================

## Reglas implementadas

* Solo los proyectos con estado Publicado pueden expirar automáticamente.
* Los proyectos eliminados lógicamente no se procesan.
* Los proyectos expirados no aparecen en el feed general.
* La expiración corre automáticamente al iniciar la API.
* La expiración corre periódicamente según la configuración.
* No se requiere migración para este bloque.

# =====================================

## Pruebas realizadas

Desde SQL Server:

* Se localizó un proyecto publicado.
* Se verificó que tenía Status = 1.
* Se forzó su CreatedAt a una fecha antigua.
* Se reinició la API.
* Se volvió a consultar el proyecto en SQL Server.
* Se confirmó que cambió de Status = 1 a Status = 7.

Desde Swagger:

* Se inició sesión con [contratista@ofipro.com](mailto:contratista@ofipro.com).
* Se probó GET /api/projects.
* Se confirmó que el proyecto expirado ya no aparecía en el feed general.

Resultado:

Bloque 11 quedó completado y probado correctamente.

# =====================================

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Completo

Bloque 8 - Calificaciones y reputación V1 → Completo

Bloque 8.1 - Endurecimiento de Ratings y reputación → Completo

Bloque 8.2 - Correcciones de diagnóstico de Ratings y reputación → Completo

Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web → Completo

Bloque 10 - ProfessionalProfile y búsqueda básica de contratistas → Completo

Bloque 10.1 - Corrección de diagnóstico de ProfessionalProfile y búsqueda de contratistas → Completo

Bloque 11 - Expiración automática de proyectos → Completo

# =====================================

## Evaluación de velocidad

Ritmo: 🟢 Bueno.

El bloque fue pequeño y avanzó rápido porque no requirió migración ni cambios en entidades. Se aprovechó que ProjectStatus.Expirado y Project.CreatedAt ya existían.

El cambio fue importante porque corrige un problema real del marketplace: evitar que los contratistas vean proyectos antiguos como si todavía fueran oportunidades disponibles.

# =====================================

## Pendiente inmediato

* Actualizar documentación.
* Revisar git status.
* Hacer commit del Bloque 11.
* Subir cambios al repositorio.

# =====================================

## Próximo bloque recomendado

Bloque 12 - Paginación y ordenamiento básico en listados críticos.

Razón:

Antes de conectar web responsiva y futura app móvil real, conviene estabilizar los endpoints que devuelven listas para evitar respuestas demasiado grandes, mejorar rendimiento y dejar contratos de API más claros.

Listados sugeridos:

* GET /api/projects
* GET /api/contractors
* GET /api/notifications
* GET /api/contracts/mine
* GET /api/proposals/my-proposals
* GET /api/projects/my-projects

# =====================================

# =====================================

# SESIÓN 2026-06-24

## Objetivo

Implementar, probar y cerrar Bloque 12 - Paginación y ordenamiento básico en listados críticos.

# =====================================

## Contexto

Después de completar la expiración automática de proyectos, se decidió avanzar con paginación porque varios endpoints críticos devolvían listas completas.

Esto podía generar problemas cuando aumentaran los proyectos, contratistas, propuestas, contratos y notificaciones.

El objetivo fue preparar mejor la API para web responsiva y futura app móvil real.

# =====================================

## Bloque 12 - Paginación y ordenamiento básico en listados críticos

Completado:

* Se creó PaginationQueryDto.
* Se creó PaginatedResponseDto<T>.
* Se organizaron los DTOs comunes dentro de carpeta Pagination.
* Se agregó paginación a GET /api/projects.
* Se agregó paginación a GET /api/contractors.
* Se agregó paginación a GET /api/notifications.
* Se agregó paginación a GET /api/contracts/mine.
* Se agregó paginación a GET /api/proposals/my-proposals.
* Se agregó paginación a GET /api/projects/my-projects.
* Se agregó conteo total para metadata de paginación.
* Se agregó ordenamiento básico por campos permitidos.
* Se mantuvieron filtros existentes de soft delete, estados activos y seguridad por usuario.

# =====================================

## Endpoints actualizados

* GET /api/projects
* GET /api/contractors
* GET /api/notifications
* GET /api/contracts/mine
* GET /api/proposals/my-proposals
* GET /api/projects/my-projects

# =====================================

## Estructura de respuesta

Los endpoints paginados devuelven:

* Items
* PageNumber
* PageSize
* TotalItems
* TotalPages
* HasPreviousPage
* HasNextPage

# =====================================

## Reglas implementadas

* PageNumber inicia en 1.
* PageSize tiene límite validado.
* SortBy se resuelve mediante campos permitidos.
* SortDirection permite ascendente o descendente.
* No se usa SQL crudo para ordenar.
* Los filtros se mantienen en LINQ con EF Core.
* Los endpoints conservan sus reglas previas de autorización y visibilidad.

# =====================================

## Pruebas realizadas

Con [cliente@ofipro.com](mailto:cliente@ofipro.com):

* GET /api/projects?pageNumber=1&pageSize=5 → 200 OK.
* GET /api/projects/my-projects?pageNumber=1&pageSize=5 → 200 OK.
* GET /api/contractors?pageNumber=1&pageSize=5 → 200 OK.
* GET /api/contractors con filtro y ordenamiento → 200 OK.
* GET /api/notifications?pageNumber=1&pageSize=5 → 200 OK.
* GET /api/notifications con ordenamiento por IsRead → 200 OK.
* GET /api/contracts/mine?pageNumber=1&pageSize=5 → 200 OK.

Con [contratista@ofipro.com](mailto:contratista@ofipro.com):

* GET /api/contracts/mine?pageNumber=1&pageSize=5 → 200 OK.
* GET /api/proposals/my-proposals?pageNumber=1&pageSize=5 → 200 OK.
* GET /api/proposals/my-proposals con ordenamiento por Status → 200 OK.

Resultado:

Bloque 12 quedó completado y probado correctamente.

# =====================================

## Observación de seguridad

OfiPro no usa motor NoSQL actualmente. La base de datos es SQL Server y las consultas se realizan principalmente mediante LINQ con EF Core.

La paginación y el ordenamiento se implementaron evitando SQL crudo y usando campos permitidos para SortBy.

Esto reduce el riesgo de inyección SQL en estos listados.

Si más adelante se agregan consultas con SQL crudo, stored procedures, MongoDB, Cosmos DB u otro motor NoSQL, se deberá abrir una revisión de seguridad específica para inyecciones y consultas dinámicas.

# =====================================

## Estado general

Bloque 1 - Fundación → Completo

Bloque 2 - Auth → Completo

Bloque 3 - Usuarios → Completo

Bloque 4 - Proyectos → Completo

Bloque 5 - Propuestas → Completo

Bloque 5.5 - Seguridad y Calidad Base → Completo

Bloque 5.6 - Limpieza de Consistencia API → Completo

Bloque 6 - Contrataciones → Completo

Bloque 6.8 - Refactor de nombres descriptivos en DTOs → Completo

Bloque 6.9 - Flujo mínimo de Contratista → Completo

Bloque 6.10 - Orden de interfaces Application → Completo

Bloque 6.11 - Correcciones de diagnóstico pre-Bloque 7 → Completo

Bloque 7 - Evidencias V1 → Completo

Bloque 7.1 - Corrección de diagnóstico de Evidencias → Completo

Bloque 7.2 - Notificaciones internas base → Completo

Bloque 8 - Calificaciones y reputación V1 → Completo

Bloque 8.1 - Endurecimiento de Ratings y reputación → Completo

Bloque 8.2 - Correcciones de diagnóstico de Ratings y reputación → Completo

Bloque 9 - Dashboard mínimo / Resúmenes para móvil y web → Completo

Bloque 10 - ProfessionalProfile y búsqueda básica de contratistas → Completo

Bloque 10.1 - Corrección de diagnóstico de ProfessionalProfile y búsqueda de contratistas → Completo

Bloque 11 - Expiración automática de proyectos → Completo

Bloque 12 - Paginación y ordenamiento básico en listados críticos → Completo

# =====================================

## Evaluación de velocidad

Ritmo: 🟢 Bueno.

El bloque fue amplio porque tocó varios endpoints, repositorios, servicios y controladores, pero avanzó bien porque se aplicó el mismo patrón en todos los listados.

No requirió migración ni cambios de dominio.

# =====================================

## Pendiente inmediato

* Actualizar documentación.
* Revisar git status.
* Hacer commit del Bloque 12.
* Subir cambios al repositorio.

# =====================================

## Próximo bloque recomendado

Bloque 13 - Pruebas automatizadas mínimas de API.

Razón:

El backend ya tiene muchos flujos críticos funcionando. Antes de seguir agregando funcionalidades grandes, conviene crear una base mínima de pruebas automatizadas para proteger autenticación, autorización, paginación y reglas principales.

Pruebas iniciales sugeridas:

* Login correcto.
* Login inválido.
* Endpoint protegido sin token.
* Endpoint protegido con rol incorrecto.
* GET /api/projects paginado.
* GET /api/contractors paginado.
* GET /api/notifications paginado.

# =====================================
