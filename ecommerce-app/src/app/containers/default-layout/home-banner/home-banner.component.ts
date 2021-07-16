import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-banner',
  templateUrl: './home-banner.component.html',
  styleUrls: ['./home-banner.component.css']
})
export class HomeBannerComponent implements OnInit {
  slides = [
    {img: "https://demo.activeitzone.com/ecommerce/public/uploads/all/qQR6fBE9MAjTWuIzGkZeI2wTDYAlIeBKQaezchPM.jpg"},
    {img: "https://demo.activeitzone.com/ecommerce/public/uploads/all/jJjPcgUsldYlpgdxpKBKmR6gIwtXIcuYtxeloijR.jpg"},
    {img: "https://demo.activeitzone.com/ecommerce/public/uploads/all/yclPDRGHySYidlrU06gics221CHFuYmZA2QvjIC2.jpg"},
    {img: "https://demo.activeitzone.com/ecommerce/public/uploads/all/TGCHVRY64CxOCh1C33Pn7mdYfYM7OW6X8JdOaN7d.jpg"}
  ];
  slideConfig = {"slidesToShow": 1, "slidesToScroll": 1, "autoplay": true, "autoplaySpeed": 2000};
  constructor() { }

  ngOnInit(): void {
  }
  addSlide() {
    this.slides.push({img: "http://placehold.it/350x150/777777"})
  }

  removeSlide() {
    this.slides.length = this.slides.length - 1;
  }

  slickInit(e) {
    //console.log('slick initialized');
  }

  breakpoint(e) {
    //console.log('breakpoint');
  }

  afterChange(e) {
    //console.log('afterChange');
  }

  beforeChange(e) {
    // console.log('beforeChange');
  }
}
