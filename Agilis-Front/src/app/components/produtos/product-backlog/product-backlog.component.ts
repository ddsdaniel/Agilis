import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Epico } from 'src/app/models/produtos/epico';
import { Feature } from 'src/app/models/produtos/feature';
import { Produto } from 'src/app/models/produtos/produto';
import { EpicoApiService } from 'src/app/services/apis/produtos/epico-api.service';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';
import { TituloService } from 'src/app/services/titulo.service';
import { BottomSheetComponent } from '../../widgets/bottom-sheet/bottom-sheet.component';

@Component({
  selector: 'app-product-backlog',
  templateUrl: './product-backlog.component.html',
  styleUrls: ['./product-backlog.component.scss']
})
export class ProductBacklogComponent implements OnInit {

  produto: Produto;
  titulo = 'Product Backlog';

  constructor(
    private activatedRoute: ActivatedRoute,
    private produtoApiService: ProdutoApiService,
    private tituloService: TituloService,
    private router: Router,
    private bottomSheetService: BottomSheetService,
    private epicoApiService: EpicoApiService,
    private featureApiService: FeatureApiService,
    private snackBar: MatSnackBar,
    private tarefaApiService: TarefaApiService,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      () => {

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.produtoApiService.obterUm(id)
          .subscribe({
            next: produto => {
              this.produto = produto;
              this.titulo = `${produto.nome} - Product Backlog`;
              this.tituloService.definir(this.titulo);
            }
          });
      });
  }

  openBottomSheetEpicos(id: string, index: number): void {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'add-feature',
        titulo: 'Adicionar Feature',
        subTitulo: 'Abre uma nova tela para adicionar uma nova feature',
        icone: 'add'
      },
      {
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui o épico',
        icone: 'clear',
        cor: '#FF0000'
      }
    ];

    this.bottomSheetService.abrir(itens, BottomSheetComponent)
      .subscribe(codigo => {
        if (codigo) {
          switch (codigo) {
            case 'add-feature':
              this.navegarParaFeature('/produtos/features/new', this.produto.epicos[index]);
              break;
            case 'editar':
              this.navegarParaEdico(`/produtos/epicos/${id}`);
              break;
            case 'excluir':
              this.excluirEpico(index);
              break;
          }
        }
      });
  }

  openBottomSheetFeatures(id: string, indiceEpico: number, indiceFeature: number): void {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'add-task',
        titulo: 'Adicionar Tarefa',
        subTitulo: 'Abre uma nova tela para adicionar uma nova tarefa',
        icone: 'add'
      },
      {
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui a feature',
        icone: 'clear',
        cor: '#FF0000'
      }
    ];

    this.bottomSheetService.abrir(itens, BottomSheetComponent)
      .subscribe(codigo => {
        if (codigo) {
          switch (codigo) {
            case 'add-task':
              this.navegarParaTarefas('/tarefas/new', this.produto.epicos[indiceEpico].features[indiceFeature]);
              break;
            case 'editar':
              this.navegarParaFeature(`/produtos/features/${id}`, this.produto.epicos[indiceEpico]);
              break;
            case 'excluir':
              this.excluirFeature(indiceEpico, indiceFeature);
              break;
          }
        }
      });
  }

  openBottomSheetTarefas(id: string, indiceEpico: number, indiceFeature: number, indiceTarefa: number): void {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui a tarefa',
        icone: 'clear',
        cor: '#FF0000'
      }
    ];

    this.bottomSheetService.abrir(itens, BottomSheetComponent)
      .subscribe(codigo => {
        if (codigo) {
          switch (codigo) {
            case 'editar':
              this.navegarParaTarefas(`/tarefas/${id}`, this.produto.epicos[indiceEpico].features[indiceFeature]);
              break;
            case 'excluir':
              this.excluirTarefa(indiceEpico, indiceFeature, indiceTarefa);
              break;
          }
        }
      });
  }

  excluirTarefa(indiceEpico: number, indiceFeature: number, indiceTarefa: number) {
    const epico = this.produto.epicos[indiceEpico];
    const feature = epico.features[indiceFeature];
    const tarefa = feature.tarefas[indiceTarefa];

    this.tarefaApiService.excluir(tarefa.id)
      .subscribe(
        () => {

          feature.tarefas.removeAt(indiceTarefa);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            const clone = Object.assign({}, tarefa);

            clone.feature = feature;
            this.tarefaApiService.adicionar(clone)
              .subscribe(
                () => {
                  feature.tarefas.insert(indiceTarefa, clone);
                },
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  excluirFeature(indiceEpico: number, indiceFeature: number) {
    const epico = this.produto.epicos[indiceEpico];
    const feature = epico.features[indiceFeature];

    this.featureApiService.excluir(feature.id)
      .subscribe(
        () => {

          epico.features.removeAt(indiceFeature);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            const clone = Object.assign({}, feature);

            clone.epico = epico;
            this.featureApiService.adicionar(clone)
              .subscribe(
                () => {
                  epico.features.insert(indiceFeature, clone);
                },
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  excluirEpico(index: number) {

    const epico = this.produto.epicos[index];

    this.epicoApiService.excluir(epico.id)
      .subscribe(
        () => {

          this.produto.epicos.removeAt(index);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            const clone = Object.assign({}, epico);

            clone.produto = this.produto;
            this.epicoApiService.adicionar(clone)
              .subscribe(
                () => {
                  this.produto.epicos.insert(index, clone);
                },
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarEpico() {
    this.navegarParaEdico('/produtos/epicos/new');
  }

  private navegarParaEdico(rota: string) {
    const params = {
      queryParams: {
        produtoId: this.produto.id
      }
    };

    this.router.navigate([rota], params);
  }

  private navegarParaFeature(rota: string, epico: Epico) {
    const params = {
      queryParams: {
        produtoId: this.produto.id,
        epicoId: epico.id,
      }
    };

    this.router.navigate([rota], params);
  }

  private navegarParaTarefas(rota: string, feature: Feature) {
    const params = {
      queryParams: {
        featureId: feature.id,
      }
    };

    this.router.navigate([rota], params);
  }
}
