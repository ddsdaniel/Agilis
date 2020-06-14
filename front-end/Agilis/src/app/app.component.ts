import { HttpErrorResponse } from '@angular/common/http';
import { AfterContentChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { menuPrincipal } from 'src/assets/constantes/menu-principal';

import { ItemMenu } from './models/menu/item-menu';
import { Time } from './models/pessoas/time';
import { TimesApiService } from './services/api/pessoas/times-api.service';
import { ProcessandoService } from './services/processando.service';
import { AutenticacaoService } from './services/seguranca/autenticacao.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterContentChecked, OnInit {

  menu: ItemMenu[] = menuPrincipal;

  constructor(
    public autenticacaoService: AutenticacaoService,
    public processandoService: ProcessandoService,
    private changeDetectorRef: ChangeDetectorRef,
    private timesApiService: TimesApiService,
    private snackBar: MatSnackBar,
  ) {

  }

  ngOnInit() {
    this.timesApiService.obterTodos()
      .subscribe(
        (times: Time[]) => this.adicionarTimesAoMenu(times),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  private adicionarTimesAoMenu(times: Time[]): void {
    {
      times.forEach(time => this.menu.push({
        icone: 'vpn',
        nome: time.nome,
        rota: `times/${time.id}`
      }));

      this.menu.push({
        icone: 'vpn',
        nome: 'Gerenciar Times',
        rota: `times`
      });

    }
  }

  ngAfterContentChecked(): void {
    this.changeDetectorRef.detectChanges();
  }
}
