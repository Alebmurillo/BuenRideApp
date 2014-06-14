class CreateRides < ActiveRecord::Migration
  def change
    create_table :rides do |t|
      #t.belongs_to :usuario
      t.string :observations
      t.string :startPointLat
      t.string :startPointLong
      t.string :destPointLat
      t.string :destPointLong
      t.integer :usuario_id

      t.timestamps
    end
  end
end
