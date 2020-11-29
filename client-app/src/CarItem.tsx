import React from 'react'
import { cars, ICar } from './demo'

interface IProps {
    car: ICar
}
const CarItem: React.FC<IProps> = ({car}) => {
    return (
        <div>
            <h1>{car.color}</h1>
        </div>
    )
}

export default CarItem
