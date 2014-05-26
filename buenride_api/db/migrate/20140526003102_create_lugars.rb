class CreateLugars < ActiveRecord::Migration
  def change
    create_table :lugars do |t|
      t.string :nombre_lugar
      t.string :lugar_latitud
      t.string :lugar_longitud

      t.timestamps
    end
  end
end
