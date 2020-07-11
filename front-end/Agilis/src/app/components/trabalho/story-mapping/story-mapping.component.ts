import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { constantes } from 'src/app/constants/constantes';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { UserStoryFK } from 'src/app/models/trabalho/user-stories/user-story-fk';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';

@Component({
  selector: 'app-story-mapping',
  templateUrl: './story-mapping.component.html',
  styleUrls: ['./story-mapping.component.scss']
})
export class StoryMappingComponent implements OnInit {

  produto: Produto = {
    id: constantes.newGuid,
    nome: '',
    atores: [],
    storyMapping: {
      temas: []
    },
    timeId: constantes.newGuid
  };

  constructor(
    private dialogoService: DialogoService,
    private produtosApiService: ProdutosApiService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        const produtoId = this.activatedRoute.snapshot.paramMap.get('produtoId');
        this.produtosApiService.obterUm(produtoId)
          .subscribe(
            (produto) => this.produto = produto,
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );
      }
    );
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

  adicionarTema() {
    this.dialogoService.abrirTexto('Entre com o nome do tema', 'Nome do tema')
      .subscribe(nome => {
        if (nome) {
          const tema: Tema = {
            id: constantes.newGuid,
            nome,
            epicos: []
          };
          this.produtosApiService.adicionarTema(this.produto.id, tema)
            .subscribe(
              (novoTema: Tema) => this.produto.storyMapping.temas.push(novoTema),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  adicionarEpico(temaId: string) {
    this.dialogoService.abrirTexto('Entre com o nome do épico', 'Nome do épico')
      .subscribe(nome => {
        if (nome) {
          this.produtosApiService.adicionarEpico(this.produto.id, temaId, nome)
            .subscribe(
              (novoEpico: Epico) => {
                const tema = this.produto.storyMapping.temas.find(t => t.id === temaId);
                tema.epicos.push(novoEpico);
              },
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  adicionarUserStory(temaId: string, epicoId: string) {
    this.dialogoService.abrirTexto('Entre com o título da user story', 'Título da user story')
      .subscribe(nome => {
        if (nome) {
          this.produtosApiService.adicionarUserStory(this.produto.id, temaId, epicoId, nome)
            .subscribe(
              (novaUserStory: UserStoryFK) => {
                const tema = this.produto.storyMapping.temas.find(t => t.id === temaId);
                const epico = tema.epicos.find(e => e.id === epicoId);
                epico.userStories.push(novaUserStory);
              },
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirTema(index: number) {
    const tema = this.produto.storyMapping.temas[index];

    this.produtosApiService.excluirTema(this.produto.id, tema.id)
      .subscribe(
        () => {

          this.produto.storyMapping.temas.removeAt(index);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.produtosApiService.adicionarTema(this.produto.id, tema)
              .subscribe(
                () => this.produto.storyMapping.temas.push(tema),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
