import { Component, OnInit } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';

@Component({
  selector: 'app-bottom-sheet',
  templateUrl: './bottom-sheet.component.html',
  styleUrls: ['./bottom-sheet.component.scss']
})
export class BottomSheetComponent implements OnInit {

  itens: BottomSheetItem[] = [];

  constructor(
    private bottomSheetRef: MatBottomSheetRef<BottomSheetComponent>,
    private bottomSheetService: BottomSheetService,
  ) { }

  ngOnInit(): void {
    this.itens = this.bottomSheetService.itens;
  }

  selecionou(event: Event, codigoSelecionado: string) {
    this.bottomSheetService.notificar(codigoSelecionado);
    this.bottomSheetRef.dismiss();
    event.preventDefault();
  }

}
