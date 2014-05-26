class CreateReviews < ActiveRecord::Migration
  def change
    create_table :reviews do |t|
      t.string :comentario
      t.string :calificacion
      t.string :submitted_by

      t.timestamps
    end
  end
end
