import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { BehaviorSubject, Observable } from 'rxjs';
import { BottomSheetItem } from '../models/bottom-sheet-item';

@Injectable({
  providedIn: 'root'
})
export class BottomSheetService {

  private selecionouSubject: BehaviorSubject<string>;

  itens: BottomSheetItem[];

  constructor(
    private bottomSheet: MatBottomSheet,
  ) { }

  abrir<T>(itens: BottomSheetItem[], component: ComponentType<T>): Observable<string> {

    this.itens = itens;

    this.bottomSheet.open(component);

    this.selecionouSubject = new BehaviorSubject<string>(null);

    return this.selecionouSubject.asObservable();
  }

  notificar(codigoSelecionado: string) {
    this.selecionouSubject.next(codigoSelecionado);
    this.selecionouSubject.complete();
  }

}
