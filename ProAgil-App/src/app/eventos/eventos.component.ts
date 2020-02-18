import { Component, OnInit } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { IEvento } from '../_modules/IEvento';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {


  eventosFiltrados: IEvento[];
  evento: IEvento;
  eventos: IEvento[];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  modoSalvar = 'Post';
  registerForm: FormGroup;
  _filtroLista: string;
  bodyDeletarEvento :string;
  titulo = "Eventos";

  constructor(private eventoService: EventoService
    , private fb: FormBuilder
    , private localeService: BsLocaleService   
    , private toastr: ToastrService     
  ) {
    this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }
  openModal(template: any) {
    this.registerForm.reset();
    template.show();

  }


  novoEvento(template: any) {
    this.modoSalvar = 'Post';
    this.openModal(template);
  }


  editarEvento(evento: IEvento, template: any) {
    this.modoSalvar = 'Put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(this.evento);    
  }

  ngOnInit() {
    this.getEventos();
    this.validation();    
  }
  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }
  salvarAlteracao(template: any) {

    if (this.registerForm.valid) {
      if (this.modoSalvar == 'Put') {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value)
        this.eventoService.putEvento(this.evento.id, this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Alterado com sucesso!');
          }, error => {
            this.toastr.error('Erro ao Salvar!');
          });
      } else {
        this.evento = Object.assign({}, this.registerForm.value)
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: IEvento) => {
            console.log(novoEvento);            
            template.hide();
            this.getEventos();   
            this.toastr.success('Cadastrado com sucesso!');         
          }, error => {
            this.toastr.error('Erro ao Salvar!');
          });
      }

    }
  }



  validation() {
    this.registerForm = this.fb.group({
      tema: ['',
        [Validators.required, Validators.minLength(4)
          , Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: [''
        , [Validators.required, Validators.max(120000)]],
      imagemUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
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
      (_eventos: IEvento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
        console.log(_eventos);
      },
      error => { this.toastr.error(`Erro ao tentar carregar eventos ${error}`); });
  }

  excluirEvento(evento: IEvento, template: any) {    
    
	this.openModal(template); 
	this.evento = evento;
	this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
}

confirmeDelete(template: any) {
	this.eventoService.deleteEvento(this.evento.id).subscribe(
		() => {
	    	template.hide();
        this.getEventos();
        this.toastr.success('Excluído com sucesso!');
	  	}, error => {
	    	this.toastr.error('Erro na exclusão!');
	  	}
	);
}
}
