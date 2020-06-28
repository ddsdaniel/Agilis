import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Jornada } from 'src/app/models/trabalho/produtos/jornada';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Fase } from 'src/app/models/trabalho/fase';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-jornadas-form',
  templateUrl: './jornadas-form.component.html',
  styleUrls: ['./jornadas-form.component.scss']
})
export class JornadasFormComponent implements OnInit {

  private produtoId: string;
  jornada: Jornada;

  constructor(
    private router: Router,
    private produtosApiService: ProdutosApiService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private dialogoService: DialogoService,
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        this.produtoId = this.activatedRoute.snapshot.paramMap.get('produtoId');
        const jornadaPosicao = parseInt(this.activatedRoute.snapshot.paramMap.get('jornadaPosicao'), 10);

        this.produtosApiService.obterJornada(this.produtoId, jornadaPosicao)
          .subscribe(
            (jornada: Jornada) => this.jornada = jornada,
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );

      }
    );
  }

  renomear() {
    this.produtosApiService.renomearJornada(this.produtoId, this.jornada.posicao, this.jornada.nome)
      .subscribe(
        // TODO: obter parametro do time
        () => this.router.navigateByUrl(`produtos/${this.produtoId}`),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoFase() {
    this.dialogoService.abrirTexto('Entre com o nome da Fase', 'Nome')
      .subscribe(nome => {
        if (nome) {
          this.produtosApiService.adicionarFase(this.produtoId, this.jornada.posicao, nome)
            .subscribe(
              (novaFase: Fase) => this.jornada.fases.push(novaFase),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}
