export enum TipoPeriodo {
  Diario = 'Diario',
  Semanal = 'Semanal',
  Quinzenal = 'Quinzenal',
  Mensal = 'Mensal',
}


export const TipoPeriodoLabel = new Map<string, string>([
  [TipoPeriodo.Diario, 'Diário'],
  [TipoPeriodo.Semanal, 'Semanal'],
  [TipoPeriodo.Quinzenal, 'Quinzenal'],
  [TipoPeriodo.Mensal, 'Mensal'],
]);
