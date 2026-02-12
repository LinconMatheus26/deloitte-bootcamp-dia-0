**Processamento de Lotes de Minério**

Utilizamos ferramentes para realizar teste nos códigos, como dbeaver, Redis e Insomnia.

Recebe os dados de um novo lote através de um endpoint POST. Realiza a persistência imediata no banco de dados PostgreSQL para garantir a segurança dos dados.
Envia uma notificação para o Redis através do RedisQueueService, criando uma chave com tempo de expiração.
