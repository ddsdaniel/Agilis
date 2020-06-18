import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Release } from 'src/app/models/trabalho/releases/release';
import { SprintFK } from 'src/app/models/trabalho/sprints/sprint-fk';
import { ReleasesApiService } from 'src/app/services/api/trabalho/releases-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
import { Fase } from 'src/app/models/trabalho/fase';
import { PrioridadeProductBacklog } from 'src/app/enums/trabalho/prioridade-product-backlog.enum';

@Component({
  selector: 'app-releases-form',
  templateUrl: './releases-form.component.html',
  styleUrls: ['./releases-form.component.scss']
})
export class ReleasesFormComponent implements OnInit {

  private timeId: string;
  release: Release;

  constructor(
    private router: Router,
    private releaseApiService: ReleasesApiService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private dialogoService: DialogoService,
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        this.timeId = this.activatedRoute.snapshot.paramMap.get('timeId');
        const releaseId = this.activatedRoute.snapshot.paramMap.get('releaseId');

        this.releaseApiService.obterUm(releaseId)
          .subscribe(
            (entidade: Release) => this.release = entidade,
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );

      }
    );
  }

  renomear() {
    this.releaseApiService.renomear(this.timeId, this.release.id, this.release.nome)
      .subscribe(
        () => this.router.navigateByUrl(`times/${this.timeId}`),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoSprint() {
    this.dialogoService.abrirTexto('Entre com o nome do Sprint', 'Nome')
      .subscribe(nome => {
        if (nome) {
          this.releaseApiService.adicionarSprint(this.release.id, nome)
            .subscribe(
              (novoSprint: SprintFK) => this.release.sprints.push(novoSprint),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirSprint(sprintId: string) {
    this.releaseApiService.excluirSprint(this.release.id, sprintId)
      .subscribe(
        () => {
          const index = this.release.sprints.findIndex(c => c.id === sprintId);
          const sprintExcluido = this.release.sprints.removeAt<SprintFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.releaseApiService.adicionarSprint(this.release.id, sprintExcluido.nome)
              .subscribe(
                (novoSprint) => this.release.sprints.insert(index, novoSprint),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoFase() {
    this.dialogoService.abrirTexto('Entre com o nome da Fase', 'Nome')
      .subscribe(nome => {
        if (nome) {
          this.releaseApiService.adicionarFase(this.release.id, nome)
            .subscribe(
              (novaFase: Fase) => this.release.productBacklog.fases.push(novaFase),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  obterNomePrioridade(prioridade: PrioridadeProductBacklog): string {
    switch (prioridade) {
      case PrioridadeProductBacklog.CaixaEntrada: return 'Caixa de Entrada';
      case PrioridadeProductBacklog.ProximoParSprints: return 'Próximos dois Sprints';
      case PrioridadeProductBacklog.ProximoSprint: return 'Próximo Sprint';
      case PrioridadeProductBacklog.SprintsFuturos: return 'Sprints Futuros';
      default: return 'Não indentificao';
    }
  }
}
