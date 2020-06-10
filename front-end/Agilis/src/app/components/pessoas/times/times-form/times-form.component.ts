import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Email } from 'src/app/models/pessoas/email';
import { Time } from 'src/app/models/pessoas/time';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';
import { UsuarioVO } from 'src/app/models/pessoas/usuario-vo';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutoVO } from 'src/app/models/trabalho/produtos/produto-vo';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent extends CrudFormComponent<Time> {

  emailAdmin: string;
  emailColaborador: string;
  nomeProduto: string;
  timeApiService: TimesApiService;

  constructor(
    router: Router,
    timeApiService: TimesApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private autenticacaoService: AutenticacaoService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
    this.timeApiService = timeApiService;
    this.sugerirNovo();
  }

  podeEditar(): boolean {
    return this.entidade.escopo === EscopoTime.Colaborativo;
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      administradores: [{
        id: this.autenticacaoService.usuarioLogado.usuario.id,
        nome: this.autenticacaoService.usuarioLogado.usuario.nome + ' ' +
          this.autenticacaoService.usuarioLogado.usuario.sobrenome
      }],
      colaboradores: [],
      produtos: [],
      escopo: EscopoTime.Colaborativo,
    };
  }

  excluirAdmin(adminId: string) {
    this.timeApiService.excluirAdmin(this.entidade.id, adminId)
      .subscribe(
        () => {
          const index = this.entidade.administradores.findIndex(a => a.id === adminId);
          this.entidade.administradores.removeAt(index);
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarAdmin() {
    const email: Email = {
      endereco: this.emailAdmin
    };
    this.timeApiService.adicionarAdmin(this.entidade.id, email)
      .subscribe(
        (novoAdmin: UsuarioVO) => {
          this.entidade.administradores.push(novoAdmin);
          this.emailAdmin = '';
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  excluirColaborador(colabId: string) {
    this.timeApiService.excluirColaborador(this.entidade.id, colabId)
      .subscribe(
        () => {
          const index = this.entidade.colaboradores.findIndex(c => c.id === colabId);
          this.entidade.colaboradores.removeAt(index);
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarColaborador() {
    const email: Email = {
      endereco: this.emailColaborador
    };
    this.timeApiService.adicionarColaborador(this.entidade.id, email)
      .subscribe(
        (novoColab: UsuarioVO) => {
          this.entidade.colaboradores.push(novoColab);
          this.emailColaborador = '';
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  excluirProduto(prodId: string) {
    this.timeApiService.excluirProduto(this.entidade.id, prodId)
      .subscribe(
        () => {
          const index = this.entidade.produtos.findIndex(p => p.id === prodId);
          this.entidade.produtos.removeAt(index);
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarProduto() {
    const produto: Produto = {
      id: constantes.newGuid,
      nome: this.nomeProduto,
      time: {
        id: this.entidade.id,
        nome: this.entidade.nome
      },
      sprints: []
    };
    this.timeApiService.adicionarProduto(this.entidade.id, produto)
      .subscribe(
        (novoProduto: ProdutoVO) => {
          this.entidade.produtos.push(novoProduto);
          this.nomeProduto = '';
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
