import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CategoriaIcones } from 'src/app/models/categoria-icones';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-icon-picker',
  templateUrl: './icon-picker.component.html',
  styleUrls: ['./icon-picker.component.scss']
})
export class IconPickerComponent {
  @Input() heading: string;
  @Input() icon: string = 'edit';
  @Input() color: string = 'red';
  @Output() event: EventEmitter<string> = new EventEmitter<string>();

  public show = false;

  private materialIcons: string[] = [];
  private popularIcons: string[] = [];
  private recentIcons: string[] = [];
  public categorias: CategoriaIcones[] = [];

  constructor(
    httpClient: HttpClient
  ) {

    this.recentIcons = JSON.parse(localStorage.getItem("icones-recentes"))
    if (this.recentIcons === null) {
      this.recentIcons = [];
    }

    this.categorias = [];
    this.categorias.push({
      categoria: 'Recentes',
      icones: this.recentIcons,
    });

    httpClient.get<string[]>(environment.popularIconsUrl)
      .subscribe(popularIcons => {
        this.popularIcons = popularIcons;

        this.categorias.push({
          categoria: 'Populares',
          icones: this.popularIcons,
        });

        httpClient.get<string[]>(environment.materialIconsUrl)
          .subscribe(materialIcons => {

            this.materialIcons = materialIcons;

            this.categorias.push({
              categoria: 'Todos',
              icones: this.materialIcons,
            });

          });

      });

  }

  salvarIconesRecentes() {
    this.recentIcons = this.recentIcons.filter(ico => ico !== this.icon);

    if (this.recentIcons.length === 10) {
      this.recentIcons.removeAt(9);
    }

    this.recentIcons.insert(0, this.icon)
    this.categorias[0].icones = this.recentIcons;

    localStorage.setItem("icones-recentes", JSON.stringify(this.recentIcons));
  }

  /**
   * Change icon from default icons
   * @param {string} icon
   */
  public changeIcon(icon: string): void {
    this.icon = icon;
    this.salvarIconesRecentes();
    this.event.emit(this.icon);
    this.show = false;
  }

  /**
   * Change status of visibility to icon picker
   */
  public toggleIcons(): void {
    this.show = !this.show;
  }
}
