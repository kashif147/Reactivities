import { Interface } from "readline";

let data = 10;

export interface ICar {
    color: string;
    model: string;
    topSpeed?: number;

}

const car1: ICar = {
    color: 'grey',
    model: 'VolksWaggon'
}

const car2: ICar= {
    color: 'blue',
    model: 'BMW',
    topSpeed: 100
}

export const cars = [car1, car2];