export enum TipoTarefa {
  Melhoria = 'Melhoria',
  Novidade = 'Novidade',
  Bug = 'Bug',
  Teste = 'Teste',
  Qualificacao = 'Qualificacao',
}

export const TipoTarefaLabel = new Map<string, string>([
  [TipoTarefa.Melhoria, 'Melhoria'],
  [TipoTarefa.Novidade, 'Novidade'],
  [TipoTarefa.Bug, 'Bug'],
  [TipoTarefa.Teste, 'Teste'],
  [TipoTarefa.Qualificacao, 'Qualificação'],
]);
