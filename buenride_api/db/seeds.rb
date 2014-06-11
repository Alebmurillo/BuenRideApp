# This file should contain all the record creation needed to seed the database with its default values.
# The data can then be loaded with the rake db:seed (or created alongside the db with db:setup).
#
# Examples:
#
#   cities = City.create([{ name: 'Chicago' }, { name: 'Copenhagen' }])
#   Mayor.create(name: 'Emanuel', city: cities.first)
Persona.create(nombre: 'Luis', email: 'luisrcarrillo2205@gmail.com' , telefono: '88671363', home_longitud: '9.02' , home_latitud: '9.5')
Persona.create(nombre: 'Sergio', email: 'sergio@gmail.com' , telefono: '98745641', home_longitud: '9.02' , home_latitud: '9.5')
Persona.create(nombre: 'Ale', email: 'ale@gmail.com' , telefono: '98765432', home_longitud: '9.02' , home_latitud: '9.5')
Persona.create(nombre: 'Roberto', email: 'roberto@gmail.com' , telefono: '98756541', home_longitud: '9.02' , home_latitud: '9.5')
Lugar.create!(nombre_lugar: 'Luis', lugar_latitud: '9.0045', lugar_longitud: '5.223')
Lugar.create!(nombre_lugar: 'san miguel', lugar_latitud: '10.0045', lugar_longitud: '8.223')