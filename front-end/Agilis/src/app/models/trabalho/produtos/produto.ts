﻿import { Entidade } from '../../entidade';
import { Time } from '../../pessoas/time';

export interface Produto extends Entidade {
  time: Time;
}
