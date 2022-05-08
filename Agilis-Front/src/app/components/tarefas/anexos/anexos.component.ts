import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Anexo } from 'src/app/models/anexo';

@Component({
  selector: 'app-anexos',
  templateUrl: './anexos.component.html',
  styleUrls: ['./anexos.component.scss']
})
export class AnexosComponent {

  novoAnexo: Anexo;
  @Input() anexos: Anexo[] = [];
  @Output() anexosChange = new EventEmitter<Anexo[]>();

  constructor(
    private snackBar: MatSnackBar,
  ) { }

  excluir(indice: number) {
    const removido = this.anexos[indice];
    this.anexos.removeAt(indice);
    this.anexosChange.emit(this.anexos);

    const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

    snackBarRef.onAction().subscribe(() => {

      this.anexos.insert(indice, removido);
      this.anexosChange.emit(this.anexos);

    });
  }

  inputFileChange(event: any) {
    this.anexar(event.target.files[0]);
  }

  anexar(file: File) {

    const MEGAS = 10;
    const MAX_SIZE = MEGAS * 1024 * 1024;

    if (file.size > MAX_SIZE) {
      this.snackBar.open(`Tamanho máximo do arquivo: ${MEGAS} MB`);

    } else {
      const reader = new FileReader();

      reader.onload = () => {

        this.novoAnexo = {
          nome: file.name,
          base64: reader.result.valueOf().toString(),
          imagem: false // TODO descobrir se é uma imagem
        };
        const clone = Object.assign({}, this.novoAnexo);
        this.anexos.push(clone);
      };
      reader.onerror = () => {
        this.snackBar.open('Erro ao carregar o anexo');
      };
      reader.readAsDataURL(file);
    }
  }
}
