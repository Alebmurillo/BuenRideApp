class CreateReviews < ActiveRecord::Migration
  def change
    create_table :reviews do |t|
      t.belongs_to :usuario
      t.string :comentario
      t.string :calificacion
      t.integer :submitted_by

      t.timestamps
    end
  end
end
