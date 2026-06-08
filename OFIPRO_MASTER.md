# OFIPRO MASTER DOCUMENT

Versión: 1.0

Fecha de creación: 2026-06-08

Estado: En desarrollo

---

# 1. VISIÓN DEL PROYECTO

## Nombre temporal

OfiPro

## Descripción

Plataforma web y móvil para conectar personas que necesitan trabajos de construcción, plomería, electricidad y otros oficios con contratistas y empresas que puedan realizarlos.

La plataforma busca convertirse en una red de confianza para contratación de oficios mediante reputación, historial y evidencia verificable.

---

# 2. PROBLEMA QUE RESUELVE

Actualmente la contratación de oficios ocurre mediante:

* Recomendaciones
* Facebook
* WhatsApp
* Amigos y familiares

Problemas detectados:

* Falta de confianza
* No existe historial verificable
* No hay evidencias estructuradas
* Difícil comparar proveedores
* Difícil encontrar trabajadores confiables

---

# 3. PROPUESTA DE VALOR

Permitir:

* Publicar proyectos
* Recibir propuestas
* Comparar contratistas
* Contratar servicios
* Evaluar resultados
* Construir reputación profesional

---

# 4. REGLAS DEL PROYECTO

## Regla 1

No aumentar alcance de una versión.

Toda idea nueva debe enviarse al backlog.

---

## Regla 2

Una versión termina cuando cumple sus objetivos.

No cuando está perfecta.

---

## Regla 3

Primero terminar V1.

Después mejorar V1.

---

## Regla 4

No discutir funcionalidades de V2 mientras V1 no esté terminada.

---

## Regla 5

Todo cambio importante debe documentarse.

---

## Regla 6

Al finalizar una sesión de trabajo se debe generar:

* Reporte de avance
* Bloque para SESSION_LOG.md
* Próximo objetivo
* Riesgos actuales
* Evaluación de velocidad

Sin importar la frase utilizada por el usuario.

Ejemplos:

* La sesión de hoy terminó
* Dejemos la sesión de hoy
* Por hoy se acabó
* Hasta aquí dejémoslo por hoy
* Mañana seguimos

---

# 5. ARQUITECTURA

## Arquitectura elegida

Clean Architecture

---

## Tipo

Monolito modular

---

## Razón

Reduce complejidad.

Permite terminar la V1 más rápido.

Facilita una futura migración a microservicios.

---

# 6. TECNOLOGÍAS

Backend

* .NET 8
* ASP.NET Core Web API

Frontend

* Angular

Base de datos

* SQL Server

ORM

* Entity Framework Core 8.0.27

Repositorio

* GitHub

---

# 7. ESTRUCTURA DE LA SOLUCIÓN

OfiPro.Api

OfiPro.Application

OfiPro.Domain

OfiPro.Infrastructure

Docs

---

# 8. MODELO DE NEGOCIO

Marketplace de oficios.

---

Usuarios objetivo:

* Personas
* Contratistas
* Empresas pequeñas

---

Oficios iniciales:

* Albañilería
* Plomería
* Electricidad

---

# 9. CONCEPTOS IMPORTANTES

## No existen clientes y contratistas separados

Todos son:

User

---

Un usuario puede:

* Publicar proyectos
* Contratar servicios

---

Si activa:

ProfessionalProfile

también puede:

* Enviar propuestas
* Aparecer en búsquedas
* Construir reputación

---

# 10. V1

Objetivo:

Permitir que un usuario:

1. Publique un proyecto
2. Reciba propuestas
3. Seleccione una propuesta
4. Finalice un trabajo
5. Califique al contratista

---

# 11. NO ENTRA EN V1

* IA
* Chat
* Pagos
* Ferreterías
* App móvil
* Empresas avanzadas
* Geolocalización avanzada
* Contratos digitales
* Facturación
* Múltiples especialidades
* Microservicios

---

# 12. ENTIDADES DOMAIN

* User
* UserRole
* ProfessionalProfile
* Category
* Subcategory
* Project
* ProjectRequirement
* ProjectPhoto
* Proposal
* Evidence
* Rating
* Invitation

---

# 13. ENUMS

UserRoleType

* Cliente
* Contratista
* Administrador

ProjectStatus

* Publicado
* Asignado
* EnProceso
* PendienteConfirmacion
* Finalizado
* Cancelado
* Expirado

ProposalStatus

* Pendiente
* Aceptada
* Rechazada
* Retirada

UrgencyLevel

* Flexible
* EstaSemana
* Urgente

EvidenceType

* Antes
* Durante
* Despues

---

# 14. DECISIONES IMPORTANTES

## D001

No usar microservicios.

Razón:

Complejidad innecesaria para V1.

---

## D002

Todos los usuarios pueden crear proyectos.

Razón:

Un contratista puede necesitar otro contratista.

---

## D003

No existirán tablas Cliente y Contratista.

Razón:

Todo gira alrededor de User.

---

## D004

ProfessionalProfile tendrá una sola especialidad.

Razón:

Reducir complejidad.

Múltiples especialidades pasan al backlog.

---

## D005

Project soportará múltiples necesidades.

Razón:

Un proyecto puede requerir varios oficios.

Resultado:

Se crea ProjectRequirement.

---

## D006

Pensar en Soft Delete desde el diseño.

Resultado:

Se agrega DeletedAt en entidades principales.

Implementación completa pendiente.

---

## D007

Documentación obligatoria.

Resultado:

OFIPRO_MASTER.md y SESSION_LOG.md.

---

# 15. PROBLEMAS DETECTADOS

## P001

Un proyecto puede requerir varios oficios.

Solución:

ProjectRequirement.

---

## P002

Los usuarios pueden cerrar acuerdos fuera de plataforma.

Solución:

Construir reputación como incentivo principal.

---

## P003

Un contratista puede necesitar contratar otro contratista.

Solución:

Todos son Users.

---

# 16. RIESGOS

## R001

Aumento constante de alcance.

Mitigación:

Backlog obligatorio.

---

## R002

Abandono del proyecto por pérdida de interés.

Mitigación:

Seguimiento por sesión.

Documentación viva.

Objetivos pequeños.

---

# 17. BACKLOG

* Múltiples especialidades
* IA
* Chat
* App móvil
* Ferreterías
* Pagos
* Empresas avanzadas
* Geolocalización
* Verificaciones
* Contratos digitales
* Facturación

---

# 18. ESTADO ACTUAL

Arquitectura:
Completada

Entidades:
Completadas

Configuraciones EF:
En progreso

Migración inicial:
Pendiente

Base de datos:
Pendiente

JWT:
Pendiente

Usuarios:
Pendiente

Proyectos:
Pendiente

Propuestas:
Pendiente
