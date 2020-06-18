import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioFK } from 'src/app/models/pessoas/usuario-FK';
import { ProdutoFK } from 'src/app/models/trabalho/produtos/produto-fk';
import { ReleaseFK } from 'src/app/models/trabalho/releases/release-fk';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent implements OnInit {

  time: Time;

  constructor(
    private router: Router,
    private timeApiService: TimesApiService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private dialogoService: DialogoService,
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        this.recuperarEntidade(id);
      }
    );
  }

  recuperarEntidade(id: string) {
    this.timeApiService.obterUm(id)
      .subscribe(
        (entidade: Time) => {
          this.time = entidade;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  podeEditar(): boolean {
    return this.time.escopo === EscopoTime.Colaborativo;
  }

  renomear() {
    this.timeApiService.renomear(this.time.id, this.time.nome)
      .subscribe(
        () => this.router.navigateByUrl('times'),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoColaborador() {
    this.dialogoService.abrirEmail()
      .subscribe(email => {
        if (email) {
          this.timeApiService.adicionarColaborador(this.time.id, email)
            .subscribe(
              (novoColab: UsuarioFK) => this.time.colaboradores.push(novoColab),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  editarRelease(id: string) {
    this.router.navigateByUrl(`releases/${this.time.id}/${id}`);
  }

  excluirColaborador(colabId: string) {
    this.timeApiService.excluirColaborador(this.time.id, colabId)
      .subscribe(
        () => {
          const index = this.time.colaboradores.findIndex(c => c.id === colabId);
          const usuarioExcluido = this.time.colaboradores.removeAt<UsuarioFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarColaborador(this.time.id, usuarioExcluido.email)
              .subscribe(
                (novoColab) => this.time.colaboradores.insert(index, novoColab),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoAdministrador() {
    this.dialogoService.abrirEmail()
      .subscribe(email => {
        if (email) {
          this.timeApiService.adicionarAdmin(this.time.id, email)
            .subscribe(
              (novoAdmin: UsuarioFK) => this.time.administradores.push(novoAdmin),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirAdministrador(adminId: string) {
    this.timeApiService.excluirAdmin(this.time.id, adminId)
      .subscribe(
        () => {
          const index = this.time.administradores.findIndex(c => c.id === adminId);
          const usuarioExcluido = this.time.administradores.removeAt<UsuarioFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarAdmin(this.time.id, usuarioExcluido.email)
              .subscribe(
                (novoAdmin) => this.time.administradores.insert(index, novoAdmin),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoProduto() {
    this.dialogoService.abrirTexto('Entre com o nome do produto', 'Nome')
      .subscribe(nome => {
        if (nome) {
          this.timeApiService.adicionarProduto(this.time.id, nome)
            .subscribe(
              (novoProduto: ProdutoFK) => this.time.produtos.push(novoProduto),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirProduto(produtoId: string) {
    this.timeApiService.excluirProduto(this.time.id, produtoId)
      .subscribe(
        () => {
          const index = this.time.produtos.findIndex(p => p.id === produtoId);
          const produtoExcluido = this.time.produtos.removeAt<ProdutoFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarProduto(this.time.id, produtoExcluido.nome)
              .subscribe(
                (novoProduto) => this.time.produtos.insert(index, novoProduto),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoRelease() {
    this.dialogoService.abrirTexto('Entre com o nome da Release', 'Nome')
      .subscribe(nome => {
        if (nome) {
          this.timeApiService.adicionarRelease(this.time.id, nome)
            .subscribe(
              (novaRelease: ReleaseFK) => this.time.releases.push(novaRelease),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirRelease(releaseId: string) {
    this.timeApiService.excluirRelease(this.time.id, releaseId)
      .subscribe(
        () => {
          const index = this.time.releases.findIndex(p => p.id === releaseId);
          const releaseExcluida = this.time.releases.removeAt<ReleaseFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluída', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarRelease(this.time.id, releaseExcluida.nome)
              .subscribe(
                (novoRelease) => this.time.releases.insert(index, novoRelease),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
