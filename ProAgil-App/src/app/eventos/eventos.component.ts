import { Component, OnInit } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { stringify } from 'querystring';
import { IEvento } from '../_modules/IEvento';



@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {


  eventosFiltrados: IEvento[];
  eventos: IEvento[];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  _filtroLista: string;
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista  ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  constructor(private eventoService: EventoService) { }

  ngOnInit() {
    this.getEventos();
  }
  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  filtrarEventos(filtrarPor: string): IEvento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
                evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }


  getEventos() {
    this.eventoService.getAllEventos().subscribe(
      (_eventos: IEvento[]) =>{
        this.eventos = _eventos; 
        this.eventosFiltrados = this.eventos;
        console.log(_eventos);
      },
        error => { console.log(error); });
  }
}
