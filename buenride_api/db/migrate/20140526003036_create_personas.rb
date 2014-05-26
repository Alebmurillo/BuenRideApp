class CreatePersonas < ActiveRecord::Migration
  def change
    create_table :personas do |t|
      t.string :nombre
      t.string :email
      t.string :telefono
      t.string :home_latitud
      t.string :home_longitud

      t.timestamps
    end
  end
end
