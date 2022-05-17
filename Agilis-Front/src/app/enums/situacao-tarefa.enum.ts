export enum SituacaoTarefa {
  AFazer = 'AFazer',
  Fazendo = 'Fazendo',
  Feito = 'Feito'
}

export const SituacaoTarefaLabel = new Map<string, string>([
  [SituacaoTarefa.AFazer, 'A Fazer'],
  [SituacaoTarefa.Fazendo, 'Fazendo'],
  [SituacaoTarefa.Feito, 'Feito'],
]);
