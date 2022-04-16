import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { DialogoInput } from 'src/app/models/dialogo-input';
import { Epico } from 'src/app/models/produtos/epico';
import { Produto } from 'src/app/models/produtos/produto';
import { EpicoApiService } from 'src/app/services/apis/produtos/epico-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { TituloService } from 'src/app/services/titulo.service';
import { DialogoInputComponent } from '../../widgets/dialogo-input/dialogo-input.component';

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
    private dialog: MatDialog,
    private epicoApiService: EpicoApiService,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      params => {

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

  abrirDialogoNovoEpico() {
    const dialogRef = this.dialog.open<DialogoInputComponent, DialogoInput>(DialogoInputComponent, {
      width: '500px',
      data: {
        titulo: 'Novo Épico',
        label: 'Nome',
        dica: '',
        valorInicial: ''
      }
    });

    dialogRef.afterClosed().subscribe({
      next: resposta => {
        if (resposta) {
          const epico: Epico = {
            id: constantes.newGuid,
            nome: resposta,
            features: [],
            produtoId: this.produto.id,
            produto: this.produto,
          };

          this.epicoApiService.adicionar(epico)
            .subscribe({
              next: id => {
                epico.id = id;
                epico.produto = null;
                this.produto.epicos.push(epico);
              },
              error: (error) => this.snackBar.open(error.message),
            });
        }
      }
    });
  }
}
