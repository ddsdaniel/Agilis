﻿import { TipoTarefa } from '../../enums/tipo-tarefa.enum';
import { Entidade } from '../entidade';
import { Feature } from '../produtos/feature';
import { UsuarioConsulta } from '../seguranca/usuario-consulta';
import { Tag } from '../tags/tag';

export interface Tarefa extends Entidade {
  titulo: string;
  descricao: string;
  feature: Feature;
  tipo: TipoTarefa;
  relator: UsuarioConsulta;
  solucionador?: UsuarioConsulta;
  horasPrevistas: string;
  horasRealizadas: string;
  tags: Tag[];
}
