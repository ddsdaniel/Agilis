import { Component, AfterContentChecked, ChangeDetectorRef } from '@angular/core';
import { menuPrincipal } from 'src/assets/constantes/menu-principal';

import { GrupoMenu } from './models/menu/grupo-menu';
import { UsuariosApiService } from './services/api/pessoas/usuarios-api.service';
import { ProcessandoService } from './services/processando.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterContentChecked {

  menu: GrupoMenu[] = menuPrincipal;

  constructor(
    public usuariosApiService: UsuariosApiService,
    public processandoService: ProcessandoService,
    private changeDetectorRef: ChangeDetectorRef,
  ) {

  }

  ngAfterContentChecked(): void {
    this.changeDetectorRef.detectChanges();
  }
}
