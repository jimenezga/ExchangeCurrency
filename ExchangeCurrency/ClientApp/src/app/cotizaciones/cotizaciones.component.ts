import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { interval } from 'rxjs/observable/interval';

@Component({
  selector: 'app-cotizaciones',
  templateUrl: './cotizaciones.component.html'
})
export class CotizacionesComponent {
  public cotizaciones: Cotizaciones[];
  public _http: HttpClient;
  public _baseUrl: string;
  public subscription: Subscription;
  public mostrarAlerta: boolean = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this._baseUrl = baseUrl;
    http.get<Cotizaciones[]>(baseUrl + 'api/Cotizacion/Exchange').subscribe(result => {
      this.cotizaciones = result;
    }, error => console.error(error));

    const source = interval(5*1000); // se deberia actualizar cada 5000 milisegundos
    this.subscription = source.subscribe(val => this.update())
  }

  public update()
  {
   this._http.get<Cotizaciones[]>(this._baseUrl + 'api/Cotizacion/Exchange').subscribe(result => {
      this.cotizaciones = result;
    }, error => console.error(error));
    if (this.mostrarAlerta == true) {
      alert("Actualizando...");
    }
  }
}

interface Cotizaciones {
  moneda: string;
  precio: number;
}
