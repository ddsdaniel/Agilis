import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor() { }

  isImage(fileName: string): boolean {
    const imageExtensions = [
      '.apng',
      '.avif',
      '.gif',
      '.jpg',
      '.jpeg',
      '.jfif',
      '.pjpeg',
      '.pjp',
      '.png',
      '.svg',
      '.webp'
    ];

    return imageExtensions.some(img => fileName.toLocaleLowerCase().endsWith(img));
  }
}
