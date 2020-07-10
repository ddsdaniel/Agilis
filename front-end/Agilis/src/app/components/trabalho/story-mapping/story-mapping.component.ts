import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
import { ActivatedRoute } from '@angular/router';
import { constantes } from 'src/app/constants/constantes';

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

  adicionarTema(){
    this.dialogoService.abrirTexto('Entre com o nome do tema', 'Nome do tema')
    .subscribe(nome => {
      if (nome) {
        this.produtosApiService.adicionarTema(this.produto.id, nome)
          .subscribe(
            (novoTema: Tema) => this.produto.storyMapping.temas.push(novoTema),
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );
      }
    });
  }

}
