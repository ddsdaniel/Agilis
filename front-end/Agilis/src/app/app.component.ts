import { AfterContentChecked, ChangeDetectorRef, Component } from '@angular/core';
import { menuPrincipal } from 'src/assets/constantes/menu-principal';

import { ItemMenu } from './models/menu/item-menu';
import { ProcessandoService } from './services/processando.service';
import { AutenticacaoService } from './services/seguranca/autenticacao.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterContentChecked {

  menu: ItemMenu[] = menuPrincipal;

  constructor(
    public autenticacaoService: AutenticacaoService,
    public processandoService: ProcessandoService,
    private changeDetectorRef: ChangeDetectorRef,
  ) {

  }

  ngAfterContentChecked(): void {
    this.changeDetectorRef.detectChanges();
  }
}
